using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VersionAPI.test
{
    /// <summary>
    /// Tests: Model Serialization
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnitTestModels
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

        /// <summary>
        /// Serialization: AssemblyVersionMetadata
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Serialization")]
        public void Test_Serialization_AssemblyInfoMetadata()
        {
            var aim = new Models.AssemblyVersionMetadata()
            {
                Company = "",
                Copyright = "2020",
                Description = "Test",
                InformationVersion = "1.0.1",
                Product = "Unit Test",
                ReleaseNotesUrl = "https://",
                SemanticVersion = "1.1.0",
                TeamEMail = "team@whatever.com",
                TeamName = "Team Name"
            };

            Libs.SerializationHelper.AssertSerialization<Models.AssemblyVersionMetadata>(_testcontext, aim);
        }

        /// <summary>
        /// Serialization: ErrorPayload
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Serialization")]
        public void Test_Serialization_ErrorPayload()
        {
            var d = new Dictionary<string, string>()
            {
                { "A", "a" },
                { "B", "b" }
            };
            var ep = new Models.ErrorPayload()
            {
                Data = d,
                Message = "Message",
                StackTrace = "Trace",
                StatusCode = 500
            };

            Libs.SerializationHelper.AssertSerialization<Models.ErrorPayload>(_testcontext, ep);
        }

    }
}
