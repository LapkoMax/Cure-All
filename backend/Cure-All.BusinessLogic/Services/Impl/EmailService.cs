using Cure_All.BusinessLogic.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(EmailOptions emailOptions)
        {
            _emailOptions = emailOptions;
        }

        public async Task SendConfirmEmail(MessageOptions message)
        {
            message.Content = string.Format("<h2 style='color: black;'>Перейдите по ссылке чтобы подтвердить ардес электронной почты:</h2><div>{0}</div><div>Эта ссылка действительна в течении 2 часов!</div>", message.Content);

            var emailMessage = CreateEmailMessage(message);

            await Send(emailMessage);
        }

        public async Task SendResetPasswordEmail(MessageOptions message)
        {
            message.Content = string.Format("<h2 style='color: black;'>Перейдите по ссылке чтобы поменять пароль:</h2><div>{0}</div><div>Эта ссылка действительна в течении 2 часов!</div>", message.Content);

            var emailMessage = CreateEmailMessage(message);

            await Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageOptions message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailOptions.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };
            return emailMessage;
        }

        private async Task Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailOptions.UserName, _emailOptions.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
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
