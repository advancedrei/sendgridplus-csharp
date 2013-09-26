using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Xml;
using System.Net.Http;

namespace SendGrid.Transport
{
    public class Web : ITransport
    {
        #region Properties
        //TODO: Make this configurable
        public const string BaseUrl = "sendgrid.com/api/";
        public const string Endpoint = "/api/mail.send";
        public const string JsonFormat = "json";
        public const string XmlFormat = "xml";

        private readonly NetworkCredential _credentials;
        private readonly bool _https;
        #endregion

        /// <summary>
        /// Factory method for Web transport of sendgrid messages
        /// </summary>
        /// <param name="credentials">SendGrid credentials for sending mail messages</param>
        /// <param name="https">Use https?</param>
        /// <returns>New instance of the transport mechanism</returns>
        public static Web GetInstance(NetworkCredential credentials, bool https = true)
        {
            return new Web(credentials, https);
        }

        /// <summary>
        /// Creates a new Web interface for sending mail.  Preference is using the Factory method.
        /// </summary>
        /// <param name="credentials">SendGrid user parameters</param>
        /// <param name="https">Use https?</param>
        internal Web(NetworkCredential credentials, bool https = true)
        {
            _https = https;
            _credentials = credentials;
        }

        /// <summary>
        /// Delivers a message over SendGrid's Web interface
        /// </summary>
        /// <param name="message"></param>
        public void Deliver(IMail message)
        {
            var client = new HttpClient
            {
                BaseAddress = _https ? new Uri("https://" + BaseUrl) : new Uri("http://" + BaseUrl)
            };

            var content = new MultipartFormDataContent();
            AttachFormParams(message, content);
            AttachAttachments(message, content);
            var response = client.PostAsync(Endpoint + ".xml", content).Result;
            CheckForErrors(response);
        }

        #region Support Methods
        private void AttachFormParams(IMail message, MultipartFormDataContent content)
        {
            var formParams = FetchFormParams(message);
            //formParams.ForEach(kvp => multipartEntity.AddBody(new StringBody(Encoding.UTF8, kvp.Key, kvp.Value)));
            foreach (var keyValuePair in formParams)
            {
                content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
            }
        }

        private void AttachAttachments(IMail message, MultipartFormDataContent content)
        {
            foreach (var attachment in message.Attachments)
            {
                StreamContent streamContent = null;

                if (attachment is FileAttachment)
                {
                    var fileAttachment = (FileAttachment)attachment;

                    var fs = new FileStream(fileAttachment.FilePath, FileMode.Open, FileAccess.Read);
                    streamContent = new StreamContent(fs);

                    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "files[" + Path.GetFileName(fileAttachment.FilePath) + "]",
                        FileName = Path.GetFileName(fileAttachment.FilePath)
                    };
                }
                else if (attachment is StreamAttachment)
                {
                    var streamAttachment = (StreamAttachment)attachment;

                    streamContent = new StreamContent(streamAttachment.Stream);

                    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = streamAttachment.Name,
                        FileName = streamAttachment.Name
                    };
                }
                else
                    throw new NotImplementedException("Unknown attachment type");
                
                streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(attachment.ContentType);
                content.Add(streamContent);
            }
        }

        private void CheckForErrors(HttpResponseMessage response)
        {
            //transport error
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.ReasonPhrase);
            }

            //TODO: check for HTTP errors... don't throw exceptions just pass info along?
            var content = response.Content.ReadAsStreamAsync().Result;

            using (var reader = XmlReader.Create(content))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "result":
                                break;
                            case "message": // success
                                bool errors = reader.ReadToNextSibling("errors");
                                if (errors)
                                    throw new ProtocolViolationException();
                                return;
                            case "error": // failure
                                throw new ProtocolViolationException();
                            default:
                                throw new ArgumentException("Unknown element: " + reader.Name);
                        }
                    }
                }
            }
        }

        internal List<KeyValuePair<string, string>> FetchFormParams(IMail message)
        {
            var result = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("api_user", _credentials.UserName),
                new KeyValuePair<string, string>("api_key", _credentials.Password),
                new KeyValuePair<string, string>("headers", message.Headers.Count == 0 ? null :  Utils.SerializeDictionary(message.Headers)),
                new KeyValuePair<string, string>("replyto", message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
                new KeyValuePair<string, string>("from", message.From.Address),
                new KeyValuePair<string, string>("fromname", message.From.DisplayName),
                new KeyValuePair<string, string>("subject", message.Subject),
                new KeyValuePair<string, string>("text", message.Text),
                new KeyValuePair<string, string>("html", message.Html),
                new KeyValuePair<string, string>("x-smtpapi", message.Header.AsJson())
            };
            if (message.To != null)
            {
                result = result.Concat(message.To.ToList().Select(a => new KeyValuePair<string, string>("to[]", a.Address)))
                    .Concat(message.To.ToList().Select(a => new KeyValuePair<string, string>("toname[]", a.DisplayName)))
                    .ToList();
            }
            if (message.Bcc != null)
            {
                result = result.Concat(message.Bcc.ToList().Select(a => new KeyValuePair<string, string>("bcc[]", a.Address)))
                        .ToList();
            }
            if (message.Cc != null)
            {
                result = result.Concat(message.Cc.ToList().Select(a => new KeyValuePair<string, string>("cc[]", a.Address)))
                    .ToList();
            }
            return result.Where(r => !string.IsNullOrEmpty(r.Value)).ToList();
        }

        #endregion
    }
}
