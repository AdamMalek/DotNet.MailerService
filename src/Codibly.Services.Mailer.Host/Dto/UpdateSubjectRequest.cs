using System.ComponentModel.DataAnnotations;

namespace Codibly.Services.Mailer.Host.Dto
{
    public class UpdateSubjectRequest
    {
        [Required] public string Subject { get; set; } 
    }
}