using System;
using System.Windows;
using Example.WP8.Annotations;
using SendGrid;
using SendGrid.Net.Mail;
using SendGrid.Transport;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

namespace Example.WP8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Private Members

        private string _userName;
        private string _password;
        private string _from;
        private string _to;
        private string _subject;
        private string _body;

        #endregion

        #region Properties

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public string From
        {
            get { return _from; }
            set
            {
                if (value == _from) return;
                _from = value;
                OnPropertyChanged();
            }
        }

        public string To
        {
            get { return _to; }
            set
            {
                if (value == _to) return;
                _to = value;
                OnPropertyChanged();
            }
        }

        public string Subject
        {
            get { return _subject; }
            set
            {
                if (value == _subject) return;
                _subject = value;
                OnPropertyChanged();
            }
        }

        public string Body
        {
            get { return _body; }
            set
            {
                if (value == _body) return;
                _body = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public bool SendMail()
        {
            try
            {
                var mail = Mail.GetInstance();
                mail.From = new MailAddress(From);
                mail.AddTo(To);
                mail.Subject = Subject;
                mail.Text = Body;
                var credentials = new NetworkCredential(UserName, Password);
                var sendGrid = Web.GetInstance(credentials);
                sendGrid.Deliver(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
