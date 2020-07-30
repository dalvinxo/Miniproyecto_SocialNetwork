using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Email
{
    public class GmailSender : IEmailSender
    {

        private readonly EmailConfiguration _emailConfiguration;

        public GmailSender(EmailConfiguration emailConfiguration) {

            _emailConfiguration = emailConfiguration;
        }

        public async Task SendMailAsync(Message message)
        {
     
            var emailMessage = CreateEmailMessage(message);

            await SendAsync(emailMessage);

        }

        private MimeMessage CreateEmailMessage(Message message) {

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) {
                Text = message.Content
        };

            return emailMessage;

        }

        private async Task SendAsync(MimeMessage mailMessage) {

            using (var client = new SmtpClient()) 
            {

                try
                {

                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.UserName, "#wakandaForever1");

                    //await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                    //await client.AuthenticateAsync("itlaprueba2@gmail.com", "#wakandaForever1");

                    await client.SendAsync(mailMessage);

                }
                catch(Exception e)
                {

                    Console.WriteLine(e);
                    throw;

                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                
                }


            }


        }


    }

}
