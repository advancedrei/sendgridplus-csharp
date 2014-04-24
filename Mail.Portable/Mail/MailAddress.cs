using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid.Net.Mail
{
    public class MailAddress
    {

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Host { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string User { get; internal set; }

        #endregion

        #region Constructors

        public MailAddress(string address)
            : this(address, null)
        {
        }

        public MailAddress(string address, string displayName)
            : this(address, displayName, Encoding.UTF8)
        {
        }

        public MailAddress(string address, string displayName, Encoding displayNameEncoding)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            if (address.Length == 0)
                throw new ArgumentException("address");

            if (displayName != null)
                this.DisplayName = displayName.Trim();
            ParseAddress(address);
        }

        #endregion

        #region Helper Methods

        void ParseAddress(string address)
        {
            // 1. Quotes for display name
            address = address.Trim();
            int idx = address.IndexOf('"');
            if (idx != -1)
            {
                if (idx != 0 || address.Length == 1)
                    throw CreateFormatException();

                int closing = address.LastIndexOf('"');
                if (closing == idx)
                    throw CreateFormatException();

                if (this.DisplayName == null)
                    this.DisplayName = address.Substring(idx + 1, closing - idx - 1).Trim();
                address = address.Substring(closing + 1).Trim();
            }

            // 2. <email>
            idx = address.IndexOf('<');
            if (idx >= 0)
            {
                if (this.DisplayName == null)
                    this.DisplayName = address.Substring(0, idx).Trim();
                if (address.Length - 1 == idx)
                    throw CreateFormatException();

                int end = address.IndexOf('>', idx + 1);
                if (end == -1)
                    throw CreateFormatException();

                address = address.Substring(idx + 1, end - idx - 1).Trim();
            }
            this.Address = address;
            // 3. email
            idx = address.IndexOf('@');
            if (idx <= 0)
                throw CreateFormatException();
            if (idx != address.LastIndexOf('@'))
                throw CreateFormatException();

            this.User = address.Substring(0, idx).Trim();
            if (User.Length == 0)
                throw CreateFormatException();
            this.Host = address.Substring(idx + 1).Trim();
            if (Host.Length == 0)
                throw CreateFormatException();
        }

        private Exception CreateFormatException()
        {
            throw new FormatException("The email address is not in the proper format.");
        }

        #endregion

        public override string ToString()
        {
            return Address;
        }
    }
}
