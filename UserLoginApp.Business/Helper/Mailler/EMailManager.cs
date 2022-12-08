using System.Net;
using System.Net.Mail;
using UserLoginApp.Business.Helper.Mailler.Model;

namespace UserLoginApp.Business.Helper.Mailler
{
    public  class EMailManager:IEMailService
    {
        private readonly EMailSettings _mailSettings;

        public EMailManager(EMailSettings emailConfig)
        {
            _mailSettings= emailConfig;
        }

        public async Task SendEmailAsync(EMailModel  mailModel)
        {
            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse(_mailSettings.From);
            //email.To.Add(MailboxAddress.Parse(mailModel.To));
            //email.Subject = mailModel.Subject;
            //var builder = new BodyBuilder();

            //builder.HtmlBody = mailModel.Body;
            //email.Body = builder.ToMessageBody();


            //using var smtp = new SmtpClient();
            //smtp.Connect(_mailSettings.SmtpServer, _mailSettings.Port, true);
            //smtp.AuthenticationMechanisms.Remove("XOAUTH2");
            //smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);


            using var message = new MailMessage(new MailAddress(_mailSettings.From, "UserLoginApp"), new MailAddress(mailModel.To))
            {

                Subject = mailModel.Subject,
                Body = mailModel.Body,
                IsBodyHtml=true
            };


            var smtp = new SmtpClient
                    {
                        Host = _mailSettings.SmtpServer,
                        Port = _mailSettings.Port,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(_mailSettings.UserName, _mailSettings.Password)
               
                     };

            smtp.Send(message);
        }
    }
}
