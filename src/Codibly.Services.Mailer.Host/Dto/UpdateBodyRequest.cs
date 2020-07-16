using System.ComponentModel.DataAnnotations;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class UpdateBodyRequest
    {
        [Required] public bool IsHtml { get; set; } 
        [Required] public string Body { get; set; } 
    }
}