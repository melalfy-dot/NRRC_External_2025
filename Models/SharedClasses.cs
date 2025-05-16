using Examine;
//using reCAPTCHA.AspNetCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Core.Models.Blocks;
using System.Dynamic;
using NCSP_Website_2024.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.Models
{
    //public static class SharedClasses
    //{
        
    //}

    public class DynamicParamters : DynamicObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            if (_properties.TryGetValue(binder.Name, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name] = value;
            return true;
        }
    }

    public class BlogsModel
    {
        //public IEnumerable<IPublishedContent> BlogItems { get; set; }

        public IPublishedContent BlogImage { get; set; }
        public IPublishedContent AuthorImage { get; set; }
        public string PageTitle { get; set; }
        public string BodyText { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDesc { get; set; }
    }

    public class ContactUsViewModel
    {

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^[a-zA-Z \\n \\r ء-ي]+$", ErrorMessageResourceName = "OnlyAlphabetic", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessageResourceName = "invalidEmail", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression("^(00)([0-9]{12})$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression(@"^(?:\d{1,3})?\d{10}$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression(@"^\d{1,3}\d{1,14}(\d{1,13})*$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Mobile { get; set; }

        //[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        //public string Subject { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Message { get; set; }
       
        [BindProperty(Name = "g-recaptcha-response")]
        public string Recaptha { get; set; }

        public bool Success { get; set; }
        public string? ResultMessage { get; set; }

        //public MessageResult MessageResult { get; set; }

        public string Culture { get; set; } = "ar-SA";
        public UmbracoHelper? Umbraco { get; set; }
        public bool IsHome { get; set; }
        public string FormName { get; set; }
        public string SendToEmail { get; set; }
        public string Subject { get; set; }
    }
    public class RequestEmployeeTrainingViewModel
    {

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^[a-zA-Z \\n \\r ء-ي]+$", ErrorMessageResourceName = "OnlyAlphabetic", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessageResourceName = "invalidEmail", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression("^(00)([0-9]{12})$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression(@"^(?:\d{1,3})?\d{10}$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression(@"^\d{1,3}\d{1,14}(\d{1,13})*$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Mobile { get; set; }

        [BindProperty(Name = "g-recaptcha-response")]
        public string Recaptha { get; set; }

        //public int TrainingFacilities { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int NumberOfEmployees { get; set; }

        //[Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string? Website { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string AreaOfTrainingRequired { get; set; }

        //public string ProgramID { get; set; }
        //public string ProgramName { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int FieldOfCompany { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int TypeOfTrainingProgram { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int TrainingFacilities { get; set; } // TrainingFacility


        public List<SelectListItem>? FieldOfCompany_List { get; set; }
        public List<SelectListItem>? TypeOfTrainingProgram_List { get; set; }
        public List<SelectListItem>? TrainingFacility_List { get; set; }

        


        public bool Success { get; set; }
        public string? ResultMessage { get; set; }

        //public MessageResult MessageResult { get; set; }



        public string Culture { get; set; } = "ar-SA";
        public UmbracoHelper? Umbraco { get; set; }
        public bool IsHome { get; set; }
        public string FormName { get; set; }
        public string SendToEmail { get; set; }
        public string Subject { get; set; }
    }
    public class ServiceStrategicPartnershipsViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int entityType { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int fieldOfCompany { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int trainingFacilities { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int targetedRegion { get; set; }



        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^[a-zA-Z \\n \\r ء-ي]+$", ErrorMessageResourceName = "OnlyAlphabetic", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string entityName { get; set; }

        //public int TrainingFacilities { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public int targetNumbers { get; set; }



        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessageResourceName = "invalidEmail", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression("^(00)([0-9]{12})$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        //[RegularExpression(@"^(?:\d{1,3})?\d{10}$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression(@"^\d{1,3}\d{1,14}(\d{1,13})*$", ErrorMessageResourceName = "InvalidMobile", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Mobile { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string theGoal { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string targetedFields { get; set; }
        

        [BindProperty(Name = "g-recaptcha-response")]
        public string Recaptha { get; set; }

        public List<SelectListItem>? EntityType_List { get; set; }
        public List<SelectListItem>? FieldOfCompany_List { get; set; }
        public List<SelectListItem>? TrainingFacility_List { get; set; }
        public List<SelectListItem>? TargetedRegion_List { get; set; }


        public bool Success { get; set; }
        public string? ResultMessage { get; set; }
        //public MessageResult MessageResult { get; set; }
        public string Culture { get; set; } = "ar-SA";
        public UmbracoHelper? Umbraco { get; set; }
        public bool IsHome { get; set; }
        public string FormName { get; set; }
        public string SendToEmail { get; set; }
        public string Subject { get; set; }
    }
    public class FilterContentModel : PublishedContentWrapped
    {
        public FilterContentModel(IPublishedContent content, IPublishedValueFallback publishedValueFallback)
          : base(content, publishedValueFallback)
        {
        }


        //public int PagesCount { get; set; }
        //public string Query { get; set; }
        //public string Page { get; set; }
        //public string Category { get; set; }
        //public string Order { get; set; }
    }

    public class FilterModel
    {
        public string PageUdi { get; set; }
        public BlockGridItem BlockGridItem { get; set; }
        public IPublishedContent Content { get; set; }

        public IEnumerable<IPublishedContent> List { get; set; }

        //public PagerViewModel Pager { get; set; }
        //public UmbracoHelper? Umbraco { get; set; }


        #region widget properties
        public string Culture { get; set; } = "ar-SA";
        public string ThemeID { get; set; } = "1";
        public string PathURL { get; set; } = "/";
        public string Paths { get; set; } = "";

        #endregion

        #region filter

        public string Keyword { get; set; }
        public string SortColumn { get; set; } = "CreatedDate";
        public string SortDirection { get; set; } = "Desc";
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        public int PageSize { get; set; } = 6;
        public int CurrentPage { get; set; } = 1;
        public int NumberOfPages { get; set; }
        public double TotalRecords { get; set; }

        //public WhereCondition GetWhereCondition()
        //{
        //    return new WhereCondition();
        //}

        #endregion

    }

    public class Pager
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
        public int TotalPages { get; set; }
        public int MaxRecords { get; set; }

        public string Query { get; set; }
        public string Keyword { get; set; }
        public string Date { get; set; }
        public string OrderByDesc { get; set; } = "1";        
        public List<int> Paths { get; set; }
        public List<SelectListItem> OrderList { get; set; }

        public string PageUdi { get; set; }
        public string PagerName { get; set; } = "umbPagerLink";
    }

    public class PortalSettings
    {
        public string PortalPath { get; set; }

        public bool IsDevelopment { get; set; }
    }
    public enum LinkType
    {
        Title,
        Link,
        SocialLink,
        ImageBeforeLink
    }

    public enum ReturnMessages
    {
        Error,
        Success,
        Warning
    }

    public enum MessageResult
    {
        [Description("Captcha Error")]
        CaptchaError = 1,
        [Description(" Insert Error")]
        InsertError = 2
    }



}
