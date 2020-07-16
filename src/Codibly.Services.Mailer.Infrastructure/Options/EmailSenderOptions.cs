namespace Codibly.Services.Mailer.Infrastructure.Options
{
    public class EmailSenderOptions
    {
        public string Host { get; set; }        
        public int Port { get; set; }        
        public string Username { get; set; }        
        public string Password { get; set; }        
        public bool UseSsl { get; set; }        
    }
}