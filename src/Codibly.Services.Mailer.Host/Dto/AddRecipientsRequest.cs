using System.ComponentModel.DataAnnotations;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class AddRecipientsRequest
    {
        [Required] public string[] Recipients { get; set; } 
    }
}