namespace Login.Models
{
    public class SMTPConfigModel
    {
        public string MyProperty { get; set; }
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSSL { get; set; }
        public string UseDefaultCredentials { get; set; }
        public bool IsBodyHTML { get; set; }


    }
}
