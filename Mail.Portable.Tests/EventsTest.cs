using System.Collections.Generic;
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
            "{" +
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

        private string payload4 = "[{\n" +
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
            "}] \n";

        private string payload5 = "[{\n" +
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

        private string payload6 = "[\r\n" +
                                  "    {\r\n" +
                                  "        \"email\": \"john.doe@sendgrid.com\",\r\n" +
                                  "        \"sg_event_id\": \"VzcPxPv7SdWvUugt-xKymw\",\r\n" +
                                  "        \"sg_message_id\": \"142d9f3f351.7618.254f56.filter-147.22649.52A663508.0\",\r\n" +
                                  "        \"timestamp\": 1386636112,\r\n" +
                                  "        \"smtp-id\": \"<142d9f3f351.7618.254f56@sendgrid.com>\",\r\n" +
                                  "        \"event\": \"processed\",\r\n" +
                                  "        \"category\":[\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"id\": \"001\",\r\n" +
                                  "        \"purchase\":\"PO1452297845\",\r\n" +
                                  "        \"uid\": \"123456\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"email\": \"not an email address\",\r\n" +
                                  "        \"smtp-id\": \"<4FB29F5D.5080404@sendgrid.com>\",\r\n" +
                                  "        \"timestamp\": 1386636115,\r\n" +
                                  "        \"reason\": \"Invalid\",\r\n" +
                                  "        \"event\": \"dropped\",\r\n" +
                                  "        \"category\":[\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"id\": \"001\",\r\n" +
                                  "        \"purchase\":\"PO1452297845\",\r\n" +
                                  "        \"uid\": \"123456\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"email\": \"john.doe@sendgrid.com\",\r\n" +
                                  "        \"sg_event_id\": \"vZL1Dhx34srS-HkO-gTXBLg\",\r\n" +
                                  "        \"sg_message_id\": \"142d9f3f351.7618.254f56.filter-147.22649.52A663508.0\",\r\n" +
                                  "        \"timestamp\": 1386636113,\r\n" +
                                  "        \"smtp-id\": \"<142d9f3f351.7618.254f56@sendgrid.com>\",\r\n" +
                                  "        \"event\": \"delivered\",\r\n" +
                                  "        \"category\":[\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"id\": \"001\",\r\n" +
                                  "        \"purchase\":\"PO1452297845\",\r\n" +
                                  "        \"uid\": \"123456\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"email\": \"john.smith@sendgrid.com\",\r\n" +
                                  "        \"timestamp\": 1386636127,\r\n" +
                                  "        \"uid\": \"123456\",\r\n" +
                                  "        \"ip\": \"174.127.33.234\",\r\n" +
                                  "        \"purchase\": \"PO1452297845\",\r\n" +
                                  "        \"useragent\": \"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36\",\r\n" +
                                  "        \"id\": \"001\",\r\n" +
                                  "        \"category\": [\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"event\": \"open\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"uid\": \"123456\",\r\n" +
                                  "        \"ip\": \"174.56.33.234\",\r\n" +
                                  "        \"purchase\":\"PO1452297845\",\r\n" +
                                  "        \"useragent\":\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36\",\r\n" +
                                  "        \"event\":\"click\",\r\n" +
                                  "        \"email\": \"john.doe@sendgrid.com\",\r\n" +
                                  "        \"timestamp\":1386637216,\r\n" +
                                  "        \"url\":\"http://www.google.com/\",\r\n" +
                                  "        \"category\":[\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"id\":\"001\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"uid\": \"123456\",\r\n" +
                                  "        \"status\": \"5.1.1\",\r\n" +
                                  "        \"sg_event_id\": \"X_C_clhwSIi4EStEpol-SQ\",\r\n" +
                                  "        \"reason\": \"550 5.1.1 The email account that you tried to reach does not exist. Please try double-checking the recipient's email address for typos or unnecessary spaces. Learn more at http: //support.google.com/mail/bin/answer.py?answer=6596 do3si8775385pbc.262 - gsmtp \",\r\n" +
                                  "        \"purchase\": \"PO1452297845\",\r\n" +
                                  "        \"event\": \"bounce\",\r\n" +
                                  "        \"email\": \"asdfasdflksjfe@sendgrid.com\",\r\n" +
                                  "        \"timestamp\": 1386637483,\r\n" +
                                  "        \"smtp-id\": \"<142da08cd6e.5e4a.310b89@localhost.localdomain>\",\r\n" +
                                  "        \"type\": \"bounce\",\r\n" +
                                  "        \"category\": [\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"id\": \"001\"\r\n" +
                                  "    },\r\n" +
                                  "    {\r\n" +
                                  "        \"email\":\"john.doe@gmail.com\",\r\n" +
                                  "        \"timestamp\":1386638248,\r\n" +
                                  "        \"uid\":\"123456\",\r\n" +
                                  "        \"purchase\":\"PO1452297845\",\r\n" +
                                  "        \"id\":\"001\",\r\n" +
                                  "        \"category\":[\"category1\",\"category2\",\"category3\"],\r\n" +
                                  "        \"event\":\"unsubscribe\"\r\n" +
                                  "    }\r\n]";

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
            var result = Events.GetEvents(payload3);
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
        public void GetEventsTest_SpaceAfterArray()
        {
            var result = Events.GetEvents(payload4);
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
        public void GetEventsTest_MissingEndBracket()
        {
            var result = Events.GetEvents(payload5);
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
        public void GetEventsTest_V3DefaultTestPayload()
        {
            var result = Events.GetEvents(payload6);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EventData>));
            Assert.IsTrue(result.Count == 7);
            Assert.IsNotNull(result[0].EventName);
            Assert.IsNotNull(result[1].EventName);
            Assert.IsNotNull(result[2].EventName);
            Assert.IsTrue(result[0].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[1].EventName == "dropped");
            Assert.IsTrue(result[2].EmailAddress == "john.doe@sendgrid.com");
            Assert.IsTrue(result[2].Categories.Count == 3);
        }

    }
}
