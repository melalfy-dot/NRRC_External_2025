using Microsoft.AspNetCore.Mvc;
using NRRC_External_2025.ViewModel;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.UmbracoContext;
using Umbraco.Cms.Web.Website.Controllers;

namespace NRRC_External_2025.Controllers
{
    public class NewsLetterController : SurfaceController
    {
        public Microsoft.AspNetCore.Mvc.ControllerBase Request { get; set; }
        private readonly IConfiguration Configuration;
        public NewsLetterController(IConfiguration configuration,
           IUmbracoContextAccessor umbracoContextAccessor,
           IUmbracoDatabaseFactory databaseFactory,
           ServiceContext services,
           AppCaches appCaches,
           IProfilingLogger profilingLogger,
           IPublishedUrlProvider publishedUrlProvider)
           : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            Configuration = configuration;
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult InsertNewsLetterMail(NewsLetterModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return (IActionResult)this.CurrentUmbracoPage();
            }

            HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            try
            {
                IContentService contentService = this.Services.ContentService;
                IContent byId = contentService.GetById(4434);
                IContent content = contentService.Create(model.Email, byId, "DT_NewsLetterItem");
                content.SetValue("email", model.Email);
                var emailExists = UmbracoContext.Content.GetById(4434).Children().Any(x => x.Name == model.Email);

                if (emailExists)
                {
                    model.Success = false;
                    return (IActionResult)this.Json((object)model);
                }
                else
                {
                    if (contentService.SaveAndPublish(content).Success)
                    {
                        model.Success = true;
                    }
                    else
                    {
                        model.Success = false;
                    }
                }


            }
            catch (Exception ex)
            {
                model.Success = false;
                return (IActionResult)this.Json((object)model);
            }
            return (IActionResult)this.Json((object)model);
        }
    }
}