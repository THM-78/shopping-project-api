using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace shopping_project_api.Data.Models.ViewModels
{
    public class VerificationResult
    {
        public required string phoneNumber { get; set; }

        [StringLength(4, MinimumLength = 4, ErrorMessage = "کد باید دقیقاً 4 کاراکتر باشد.")]
        [Required]
        public string code { get; set; }

        public required int messageId { get; set; }
    }
}
