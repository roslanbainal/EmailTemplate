using EmailApp.Data;
using EmailApp.DTOs;
using EmailApp.Infrastructure.Interfaces;
using EmailApp.Models;
using Hangfire;
using Hangfire.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailApp.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigDto _smtpConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<SMTPConfigDto> smtpConfig,ILogger<EmailService> logger)
        {
            _smtpConfig = smtpConfig.Value;
            _logger = logger;
        }
        public async Task SendTestEmail(UserEmailOptionsDto userEmailOptions)
        {

            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, This is test email subject from Simple Advantage Sdn Bhd", userEmailOptions.PlaceHolders);

            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptionsDto userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }


        private string GetEmailBody(string templateName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "EmailView");
            var body = File.ReadAllText(string.Format(path + "//{0}.html", templateName));
            return body;
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        public async Task<List<MstUser>> CheckingEmailInactive()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var usersInactive = await context.MstUsers.AsNoTracking().Where(m => m.LastLogin < DateTime.Now.AddDays(-181) && m.UserId == 1221).ToListAsync();
                    return usersInactive;
                }
            }
            catch (Exception ex) { 
                Console.WriteLine("Outer Exception: " + ex.Message);
                throw;
            }


        }

        public async Task TestSendEmail()
        {
            var users = await CheckingEmailInactive();
            List<UserInfoDto> userInfoDto = new List<UserInfoDto>();
            users.ForEach(x => userInfoDto.Add(new UserInfoDto { Email = x.Email, FullName = x.FullName }));
            foreach (var item in userInfoDto)
            {
                UserEmailOptionsDto options = new UserEmailOptionsDto
                {
                    ToEmails = new List<string> { item.Email},
                    PlaceHolders = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("{{Email}}", item.Email),
                            new KeyValuePair<string, string>("{{FullName}}", item.FullName),
                        },
                };
                options.Subject = UpdatePlaceHolders("Email Notifications", options.PlaceHolders);
                options.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), options.PlaceHolders);
                _logger.LogInformation("From TestSendEmail : User {email}", item.Email);
                await SendEmail(options);
            }

        }
    }
}
