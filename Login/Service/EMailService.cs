using Login.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace Login.Service
{
    public class EMailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly IOptions<SMTPConfigModel> _smtpconfig;

        public EMailService(IOptions<SMTPConfigModel> smtpconfig)
        {
            _smtpconfig = smtpconfig;
        }
        private async Task SendEmail(EmailOptionModel emailOption)
        {
            MailMessage mail = new MailMessage
            {
                Subject=emailOption.Subject,
                Body=emailOption.Body,
                
            };
        }
        private string EmailBody(string Tempatename)
        {
            var body=File.ReadAllText(string.Format(templatePath, Tempatename));
            return body;
        }
    }
}
