using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Codelux.Common.Extensions;

namespace Codelux.Utilities.Mailing
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpData _smtpData;
        private readonly SmtpCredentials _credentials;

        public SmtpEmailSender(SmtpData smtpData, SmtpCredentials credentials)
        {
            smtpData.Guard((nameof(smtpData)));
            credentials.Guard((nameof(credentials)));

            _smtpData = smtpData;
            _credentials = credentials;
        }

        public bool SendEmail(Email email)
        {
            ValidateEmail(email);

            try
            {
                using SmtpClient client = new(_smtpData.Host, _smtpData.Port)
                {
                    EnableSsl = _smtpData.UseTls,
                    Credentials = new NetworkCredential(_credentials.Username, _credentials.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                foreach (string recipient in email.To)
                {
                    using MailMessage message =
                        new(new(email.From), new MailAddress(recipient))
                        {
                            IsBodyHtml = email.IsHtml
                        };

                    foreach (string replyTo in email.ReplyTo)
                        message.ReplyToList.Add(new MailAddress(replyTo));

                    message.Body = email.Body;
                    message.Subject = email.Subject;

                    client.Send(message);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> SendEmailAsync(Email email, CancellationToken token = default)
        {
            ValidateEmail(email);

            try
            {
                using SmtpClient client = new(_smtpData.Host, _smtpData.Port)
                {
                    EnableSsl = _smtpData.UseTls,
                    Credentials = new NetworkCredential(_credentials.Username, _credentials.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                foreach (string recipient in email.To)
                {
                    using MailMessage message =
                        new(new(email.From, email.FromName), new MailAddress(recipient))
                        {
                            IsBodyHtml = email.IsHtml
                        };

                    foreach (string replyTo in email.ReplyTo)
                        message.ReplyToList.Add(new MailAddress(replyTo));

                    message.Body = email.Body;
                    message.Subject = email.Subject;

                    await client.SendMailAsync(message, token).ConfigureAwait(false);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void ValidateEmail(Email email)
        {
            email.Guard(nameof(email));
            email.From.Guard(nameof(email));

            if (email.To.Count == 0) throw new("You are attempting to send an e-mail to no recipients.");

            if (email.ReplyTo.Count == 0)
                email.ReplyTo.Add(email.From);
        }
    }
}
