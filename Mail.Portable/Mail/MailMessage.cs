using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid.Net.Mail
{
    public class MailMessage
    {

        /// <summary>
        /// 
        /// </summary>
        public AlternateViewCollection AlternateViews { get; internal set; }
        
        /// <summary>
        /// 
        /// </summary>
        public AttachmentCollection Attachments { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public MailAddressCollection Bcc { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public MailAddressCollection CC { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public MailAddress From { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Headers { get; internal set; } 

        /// <summary>
        /// 
        /// </summary>
        public string Html { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public MailAddressCollection ReplyToList { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MailAddressCollection To { get; set; }


        #region Constructors

        public MailMessage()
        {
            this.To = new MailAddressCollection();

            AlternateViews = new AlternateViewCollection();
            Attachments = new AttachmentCollection();
            Bcc = new MailAddressCollection();
            CC = new MailAddressCollection();
            ReplyToList = new MailAddressCollection();
            Headers = new Dictionary<string, string> {{"MIME-Version", "1.0"}};
        }

        // FIXME: should it throw a FormatException if the addresses are wrong? 
        // (How is it possible to instantiate such a malformed MailAddress?)
        public MailMessage(MailAddress from, MailAddress to)
            : this()
        {
            if (from == null || to == null)
                throw new ArgumentNullException();

            From = from;

            this.To.Add(to);
        }

        public MailMessage(string from, string to)
            : this()
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException("from");
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException("to");

            this.From = new MailAddress(from);
            foreach (string recipient in to.Split(new char[] { ',' }))
                this.To.Add(new MailAddress(recipient.Trim()));
        }

        public MailMessage(string from, string to, string subject, string body)
            : this()
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException("from");
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException("to");

            this.From = new MailAddress(from);
            foreach (string recipient in to.Split(new char[] { ',' }))
                this.To.Add(new MailAddress(recipient.Trim()));

            //Body = body;
            Subject = subject;
        }

        #endregion // Constructors


                //new KeyValuePair<string, string>("api_user", _credentials.UserName),
                //new KeyValuePair<string, string>("api_key", _credentials.Password),
                //new KeyValuePair<string, string>("headers", message.Headers.Count == 0 ? null :  Utils.SerializeDictionary(message.Headers)),
                //new KeyValuePair<string, string>("replyto", message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
                //new KeyValuePair<string, string>("from", message.From.Address),
                //new KeyValuePair<string, string>("fromname", message.From.DisplayName),
                //new KeyValuePair<string, string>("subject", message.Subject),
                //new KeyValuePair<string, string>("text", message.Text),
                //new KeyValuePair<string, string>("html", message.Html),
                //new KeyValuePair<string, string>("x-smtpapi", message.Header.AsJson())





    }
}
