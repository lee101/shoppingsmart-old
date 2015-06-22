using ShoppingSmart.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Drawing;

namespace TestShoppingSmart
{
    
    
    /// <summary>
    ///This is a test class for SSUtilsTest and is intended
    ///to contain all SSUtilsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SSUtilsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for DownloadImage
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void DownloadImageTest()
        {
            string _URL = string.Empty; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            Image expected = null; // TODO: Initialize to an appropriate value
            Image actual;
            actual = SSUtils.DownloadImage(_URL, name);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StripHTML
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void StripHTMLTest()
        {
            string text = "<br> asdf <hey h=\"j\">f</hey>"; // TODO: Initialize to an appropriate value
            string expected = " asdf f"; // TODO: Initialize to an appropriate value
            string actual;
            actual = SSUtils.StripHTML(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StripInvalidPathChars
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void StripInvalidPathCharsTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = SSUtils.StripInvalidPathChars(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for StripPunctuation
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void StripPunctuationTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = SSUtils.StripPunctuation(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for getFirstStringBetween
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void getFirstStringBetweenTest()
        {
            string text = string.Empty; // TODO: Initialize to an appropriate value
            string start = string.Empty; // TODO: Initialize to an appropriate value
            string end = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = text.getFirstStringBetween(start, end);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for correctSpelling
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void correctSpellingTest()
        {
            string text = "red dres"; // TODO: Initialize to an appropriate value
            string expected = "red dress"; // TODO: Initialize to an appropriate value
            string actual;
            actual = SSUtils.correctSpelling(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for hasCorrectSpelling
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\user-000\\documents\\visual studio 2010\\Projects\\ShoppingSmart\\ShoppingSmart", "/")]
        [UrlToTest("http://localhost:8018/")]
        public void hasCorrectSpellingTest()
        {
            string text = "dres"; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = SSUtils.hasCorrectSpelling(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
