namespace Pingvinius.Framework.Mailing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Mail;
    using NLog;

    /// <summary>
    /// The mail helper.
    /// Sends messages via default configured in Web.config SMTP client.
    /// </summary>
    public static class MailHelper
    {
        /// <summary>
        /// The logger for mail helper.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sends the specified mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns>Result of sending.</returns>
        /// <exception cref="System.ArgumentNullException">mail</exception>
        public static bool Send(Mail mail)
        {
            if (mail == null)
            {
                throw new ArgumentNullException("mail");
            }

            return MailHelper.Send(new List<Mail>() { mail })[0];
        }

        /// <summary>
        /// Sends mails.
        /// </summary>
        /// <param name="mails">The mails.</param>
        /// <returns>Array of results for each mail.</returns>
        public static bool[] Send(IEnumerable<Mail> mails)
        {
            if (mails == null || mails.Count() == 0)
            {
                return new bool[0];
            }

            MailMessage mailMessage = null;
            Mail[] mailArray = mails.ToArray();
            bool[] results = new bool[mailArray.Length];

            using (var smtpServer = new SmtpClient())
            {
                for (int i = 0; i < mailArray.Length; i++)
                {
                    var mail = mailArray[i];

                    // Validate mail
                    if (!MailHelper.ValidateMail(mail))
                    {
                        MailHelper.Logger.Warn(string.Format("Validating mail '{0}' to {1} failed.", mail.Subject, string.Join(", ", mail.Receivers)));
                        continue;
                    }

                    try
                    {
                        // Build mail message
                        mailMessage = new MailMessage()
                        {
                            From = new MailAddress(mail.SenderEmail, mail.SenderName),
                            Subject = mail.Subject,
                            SubjectEncoding = mail.Encoding,
                            Body = mail.Body,
                            BodyEncoding = mail.Encoding,
                            IsBodyHtml = mail.IsBodyHtml,
                            Priority = MailPriority.Normal
                        };

                        // Fill up receivers
                        foreach (var receiver in mail.Receivers)
                        {
                            mailMessage.To.Add(new MailAddress(receiver));
                        }

                        // Send message
                        smtpServer.Send(mailMessage);

                        // Set up success flag
                        results[i] = true;
                    }
                    catch (Exception ex)
                    {
                        MailHelper.Logger.Error("The sending mail operation failed.", ex);
                    }
                    finally
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Dispose();
                            mailMessage = null;
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Validates the mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns>True if the mail is correct; otherwise false.</returns>
        /// <exception cref="System.ArgumentNullException">mail</exception>
        private static bool ValidateMail(Mail mail)
        {
            if (mail == null)
            {
                throw new ArgumentNullException("mail");
            }

            return !string.IsNullOrWhiteSpace(mail.SenderEmail) && !string.IsNullOrWhiteSpace(mail.SenderName) &&
                !string.IsNullOrWhiteSpace(mail.Subject) && !string.IsNullOrWhiteSpace(mail.Body) &&
                mail.Receivers.Count() > 0 && mail.Receivers.All(r => string.IsNullOrWhiteSpace(r));
        }
    }
}