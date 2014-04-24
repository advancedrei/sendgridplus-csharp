using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid
{
    public class EventData
    {

        /// <summary>
        /// The number of times this delivery has been attempted.
        /// </summary>
        /// <remarks>Applies to the Deferred event.</remarks>
        public int Attempt { get; set; }

        /// <summary>
        /// The categories assigned to the message.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("category")]
        [JsonConverter(typeof(SendGridCategoryConverter))]
        public List<string> Categories { get; set; }

        /// <summary>
        /// The Email address of the intended recipient.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Applies to all message types.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("event")]
        public string EventName { get; set; }

        /// <summary>
        /// The IP Address where the event originated.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("ip")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Bounce or Drop reason from MTA
        /// </summary>
        /// <remarks>Applies to only the Bounce and Drop events.</remarks>
        public string Reason { get; set; }

        /// <summary>
        /// Full reponse from MTA.
        /// </summary>
        /// <remarks>Applies to only the Deferred and Delivered events.</remarks>
        public string Response { get; set; }

        /// <summary>
        /// A unique id for the event, assigned by SendGrid. This is a 23 byte string.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("sg_event_id")]
        public string SendGridId { get; set; }

        /// <summary>
        /// An id attached to the message by the originating system.
        /// </summary>
        /// <remarks>Applies to all message types. Optional.</remarks>
        [JsonProperty("smtp-id")]
        public string SmtpId { get; set; }

        /// <summary>
        /// 3 digit status code.
        /// </summary>
        /// <remarks>Applies to only the Bounce event.</remarks>
        public string Status { get; set; }

        /// <summary>
        /// The Unix Time that the event occurred.
        /// </summary>
        public Int64 TimeStamp { get; set; }

        /// <summary>
        /// Bounce/Blocked/Expired
        /// </summary>
        /// <remarks>Applies to only the Bounce event.</remarks>
        public string Type { get; set; }

        /// <summary>
        /// A set of unique arguments sent through the SMTP API.
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        [JsonProperty("unique_args")]
        public Dictionary<string, string> UniqueArguments { get; set; }

        /// <summary>
        /// The URL that was clicked.
        /// </summary>
        /// <remarks>Applies to only the Click event.</remarks>
        public string Url { get; set; }

        /// <summary>
        /// The user agent responsible for the event
        /// </summary>
        /// <remarks>Applies to all message types.</remarks>
        public string UserAgent { get; set; }

    }
}
