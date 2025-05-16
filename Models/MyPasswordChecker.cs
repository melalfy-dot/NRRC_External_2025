//using Umbraco.Cms.Core.Security;
//namespace NetCore.Models;
//public class MyPasswordChecker : IBackOfficeUserPasswordChecker
//{
//    public Task<BackOfficeUserPasswordCheckerResult> CheckPasswordAsync(BackOfficeIdentityUser user, string password)
//    {
//        var result = (password == "test")
//            ? Task.FromResult(BackOfficeUserPasswordCheckerResult.ValidCredentials)
//            : Task.FromResult(BackOfficeUserPasswordCheckerResult.InvalidCredentials);
//        return result;
//    }
//}

using System.DirectoryServices.AccountManagement;
using Umbraco.Cms.Core.Security;

namespace NetCore.Models
{
    public class MyPasswordChecker : IBackOfficeUserPasswordChecker
    {
        private readonly IConfiguration Configuration;

        public MyPasswordChecker(IConfiguration configuration) => this.Configuration = configuration;

        public Task<BackOfficeUserPasswordCheckerResult> CheckPasswordAsync(
          BackOfficeIdentityUser user,
          string password)
        {
            Task<BackOfficeUserPasswordCheckerResult> task = Task.FromResult(BackOfficeUserPasswordCheckerResult.FallbackToDefaultChecker);

            try
            {
                if (user != null && !string.IsNullOrEmpty(user.Name)
                    //&& !user.Name.ToLower().Contains("@")
                    )
                {
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain,
                   Configuration.GetSection("ADSettings").GetValue<string>("DomainName"), null, ContextOptions.Negotiate,
                   Configuration.GetSection("ADSettings").GetValue<string>("DomainUserName"),
                   Configuration.GetSection("ADSettings").GetValue<string>("DomainUserPassword"))
                        )
                    {
                        try
                        {
                            if (UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, user.Name) != null)
                            {
                                task = context.ValidateCredentials(user.Name, password) ?
                                                                   Task.FromResult(BackOfficeUserPasswordCheckerResult.ValidCredentials) :
                                                                   Task.FromResult(BackOfficeUserPasswordCheckerResult.InvalidCredentials);

                                if (task.Result == BackOfficeUserPasswordCheckerResult.InvalidCredentials)
                                {
                                    // if the user password is incorrect, then check for local account
                                    task = Task.FromResult(BackOfficeUserPasswordCheckerResult.FallbackToDefaultChecker);
                                }
                            }
                            else
                            {
                                SharedHelper.LogInformation("login user not found");
                            }
                        }
                        catch (Exception ex)
                        {
                            SharedHelper.LogException(ex);
                        }
                    }
                }
                else
                {
                    SharedHelper.LogInformation("user == null");
                }
            }
            catch (Exception ex)
            {
                SharedHelper.LogException(ex);
            }

            return task;
        }
    }

}
