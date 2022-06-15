namespace ecovat_service.businessLogic.Models
{
    public class EmailServiceSettings
    {
        public EmailServiceSettings()
        {
            MailTo = new List<string>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public List<string> MailTo { get; set; }
    }
}
