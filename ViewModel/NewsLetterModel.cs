using NRRC_External_2025.Resources;
using System.ComponentModel.DataAnnotations;

namespace NRRC_External_2025.ViewModel
{
    public class NewsLetterModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(ValidationRSS))]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessageResourceName = "invalidEmail", ErrorMessageResourceType = typeof(ValidationRSS))]
        public string Email { get; set; }
        public bool Success { get; set; }

    }
}
