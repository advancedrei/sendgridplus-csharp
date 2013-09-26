using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Mail;

namespace SendGrid.Tests
{
    [TestClass]
    public class TestHeader
    {
        [TestMethod]
        public void TestAddSubVal()
        {
            var test = new Header();
            test.AddSubVal("foo", new List<string>{"bar", "raz"});
            var result = test.AsJson();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"bar\", \"raz\"]}}", result);            
        }

        [TestMethod]
        public void TestAddUniqueIdentifier()
        {
            var test = new Header();
            test.AddUniqueIdentifier(new Dictionary<string, string>(){{"foo", "bar"}});
            var result = test.AsJson();
            Assert.AreEqual("{\"unique_args\" : {\"foo\" : \"bar\"}}", result);
        }

        [TestMethod]
        public void TestSetCategory()
        {
            var test = new Header();
            test.SetCategory("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"category\" : \"foo\"}", result);
        }

        [TestMethod]
        public void TestSetCategories()
        {
            var test = new Header();
            test.SetCategories(new List<string>{"dogs","animals","pets","mammals"});
            var result = test.AsJson();
            Assert.AreEqual("{\"category\" : [\"dogs\", \"animals\", \"pets\", \"mammals\"]}", result);
        }

        [TestMethod]
        public void TestEnable()
        {
            var test = new Header();
            test.Enable("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"1\"}}}}", result); 
        }

        [TestMethod]
        public void TestDisable()
        {
            var test = new Header();
            test.Disable("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"0\"}}}}", result);
        }

        [TestMethod]
        public void TestAddFilterSetting()
        {
            var test = new Header();
            test.AddFilterSetting("foo", new List<string> { "a", "b" }, "bar");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"a\" : {\"b\" : \"bar\"}}}}}", result);
            
        }

        [TestMethod]
        public void TestAddHeader()
        {
            var test = new Header();
            test.AddSubVal("foo", new List<string> { "a", "b" });
            var mime = new MailMessage();
            test.AddHeader(mime);
            var result = mime.Headers.Get("x-smtpapi");
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"a\", \"b\"]}}", result);
        }

        [TestMethod]
        public void TestAsJson()
        {
            var test = new Header();
            var result = test.AsJson();
            Assert.AreEqual("", result);

            test = new Header();
            test.AddSubVal("foo", new List<string>{"a", "b"});
            result = test.AsJson();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"a\", \"b\"]}}", result);
        }
    }
}
