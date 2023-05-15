namespace Pingvinius.Framework.Mailing
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The class represents mail entity.
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// Gets or sets the encoding.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        /// <value>
        /// The name of the sender.
        /// </value>
        public string SenderName { get; set; }

        /// <summary>
        /// Gets or sets the sender email.
        /// </summary>
        /// <value>
        /// The sender email.
        /// </value>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Gets or sets the receivers.
        /// </summary>
        /// <value>
        /// The receivers.
        /// </value>
        public IEnumerable<string> Receivers { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this mail's body is in HTML.
        /// </summary>
        /// <value>
        /// <c>true</c> if the mail's body is in HTML; otherwise, <c>false</c>.
        /// </value>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mail"/> class.
        /// </summary>
        public Mail()
        {
            this.Encoding = Encoding.UTF8;
            this.SenderName = string.Empty;
            this.SenderEmail = string.Empty;
            this.Receivers = new List<string>();
            this.Subject = string.Empty;
            this.Body = string.Empty;
            this.IsBodyHtml = false;
        }
    }
}