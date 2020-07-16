using System.ComponentModel.DataAnnotations;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class UpdateSenderRequest
    {
        [Required] public string Sender { get; set; } 
    }
}