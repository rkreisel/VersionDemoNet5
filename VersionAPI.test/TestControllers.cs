using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VersionAPI.Libs;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace VersionAPI.test
{
    /// <summary>
    /// Test: Controllers
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TestControllers
    {
        private static TestContext _testcontext;

        /// <summary>
        /// Test: Setup
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void SetUpTests(TestContext testContext)
        {
            _testcontext = testContext;
        }

        #region "Common Controller"

        /// <summary>
        /// Common: Version
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_Common_VersionGet()
        {
            var logger = new Libs.MsTestLogger<Controllers.CommonController>(_testcontext);
            var cc = new Controllers.CommonController(logger);
            var iResult = cc.VersionGet();
            Assert.IsNotNull(iResult);
        }

        /// <summary>
        /// Common: Version Details
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_Common_VersionDetailsGet()
        {
            var logger = new Libs.MsTestLogger<Controllers.CommonController>(_testcontext);
            var cc = new Controllers.CommonController(logger);
            var iResult = cc.VersionDetailsGet();
            Assert.IsNotNull(iResult);
        }

        /// <summary>
        /// Common: Health Check
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_Common_HealthCheckGet()
        {
            var logger = new Libs.MsTestLogger<Controllers.CommonController>(_testcontext);
            var cc = new Controllers.CommonController(logger);
            var iResult = cc.CheckHealth();
            Assert.IsNotNull(iResult);
        }

        #endregion

        #region "V1"

        /// <summary>
        /// DummyV1: Get
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_DummyV1_DummyGet()
        {
            var logger = new Libs.MsTestLogger<Controllers.DummyControllerV1>(_testcontext);
            var dc = new Controllers.DummyControllerV1(logger);
            var iResult = dc.DummyGet();
            Assert.IsNotNull(iResult);
        }

        /// <summary>
        /// DummyV1: Post
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_DummyV1_DummyPost()
        {
            var logger = new Libs.MsTestLogger<Controllers.DummyControllerV1>(_testcontext);
            var dc = new Controllers.DummyControllerV1(logger);
            var iResult = dc.DummyPost("Dummy");
            Assert.IsNotNull(iResult);
        }

        #endregion

        #region "V2"

        /// <summary>
        /// DummyV2: Get
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_DummyV2_DummyGet()
        {
            var logger = new Libs.MsTestLogger<Controllers.DummyControllerV2>(_testcontext);
            var dc = new Controllers.DummyControllerV2(logger);
            var iResult = dc.DummyGet();
            Assert.IsNotNull(iResult);
        }

        /// <summary>
        /// DummyV2: Post
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_DummyV2_DummyPost()
        {
            var logger = new Libs.MsTestLogger<Controllers.DummyControllerV2>(_testcontext);
            var dc = new Controllers.DummyControllerV2(logger);
            var iResult = dc.DummyPost("Dummy");
            Assert.IsNotNull(iResult);
        }


        /// <summary>
        /// DummyV2: List
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Controller")]
        public void Test_Ctrl_DummyV2_DummyList()
        {
            var logger = new Libs.MsTestLogger<Controllers.DummyControllerV2>(_testcontext);
            var dc = new Controllers.DummyControllerV2(logger);
            var iResult = dc.DummyList();
            Assert.IsNotNull(iResult);
        }

        #endregion
    }
}
