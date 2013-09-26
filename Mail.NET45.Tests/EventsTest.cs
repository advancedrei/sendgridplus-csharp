using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SendGrid.Tests
{

    [TestClass]
    public class EventsTest
    {

        #region Private Members

        private string payload = "[\n" +
            "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337197600,\n" +
                "\"smtp-id\": \"<4FB4041F.6080505@sendgrid.com>\",\n" +
                "\"event\": \"processed\",\n" +
            "},\n" +
            "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337966815,\n" +
                "\"smtp-id\": \"<4FBFC0DD.5040601@sendgrid.com>\",\n" +
                "\"category\": \"newuser\",\n" +
                "\"event\": \"clicked\"\n" +
            "}, " +
            "{"+
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337969592,\n" +
                "\"smtp-id\": \"<20120525181309.C1A9B40405B3@Example-Mac.local>\",\n" +
                "\"category\": [\"somestring1\",\"somestring2\"],\n" +
                "\"event\": \"processed\",\n" +
            "}\n" +
        "]";

        private string payload2 = "[\n" +
            "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337197600,\n" +
                "\"smtp-id\": \"<4FB4041F.6080505@sendgrid.com>\",\n" +
                "\"event\": \"processed\",\n" +
            "},\n" +
            "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337966815,\n" +
                "\"smtp-id\": \"<4FBFC0DD.5040601@sendgrid.com>\",\n" +
                "\"category\": \"newuser\",\n" +
                "\"event\": \"clicked\"\n" +
            "}, " +
            "{" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337969592,\n" +
                "\"smtp-id\": \"<20120525181309.C1A9B40405B3@Example-Mac.local>\",\n" +
                "\"category\": [\"somestring1\",\"somestring2\"],\n" +
                "\"event\": \"processed\",\n" +
            "}\n";

        private string payload3 = "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337197600,\n" +
                "\"smtp-id\": \"<4FB4041F.6080505@sendgrid.com>\",\n" +
                "\"event\": \"processed\",\n" +
            "},\n" +
            "{\n" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337966815,\n" +
                "\"smtp-id\": \"<4FBFC0DD.5040601@sendgrid.com>\",\n" +
                "\"category\": \"newuser\",\n" +
                "\"event\": \"clicked\"\n" +
            "}, " +
            "{" +
                "\"email\": \"john.doe@sendgrid.com\",\n" +
                "\"timestamp\": 1337969592,\n" +
                "\"smtp-id\": \"<20120525181309.C1A9B40405B3@Example-Mac.local>\",\n" +
                "\"category\": [\"somestring1\",\"somestring2\"],\n" +
                "\"event\": \"processed\",\n" +
            "}\n";

        #endregion

        [TestMethod]
        public void GetEventsTest1()
        {
            var result = Events.GetEvents(payload);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EventData>));
            Assert.IsTrue(result.Count == 3);
            Assert.IsNotNull(result[0].EventName);
            Assert.IsNotNull(result[1].EventName);
            Assert.IsNotNull(result[2].EventName);
            Assert.IsTrue(result[0].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[1].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].Categories.Count == 2);
        }

        [TestMethod]
        public void GetEventsTest2()
        {
            var result = Events.GetEvents(payload2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EventData>));
            Assert.IsTrue(result.Count == 3);
            Assert.IsNotNull(result[0].EventName);
            Assert.IsNotNull(result[1].EventName);
            Assert.IsNotNull(result[2].EventName);
            Assert.IsTrue(result[0].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[1].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].Categories.Count == 2);
        }
        [TestMethod]
        public void GetEventsTest3()
        {
            var result = Events.GetEvents(payload2);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EventData>));
            Assert.IsTrue(result.Count == 3);
            Assert.IsNotNull(result[0].EventName);
            Assert.IsNotNull(result[1].EventName);
            Assert.IsNotNull(result[2].EventName);
            Assert.IsTrue(result[0].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[1].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].Categories.Count == 2);
        }

    }
}
