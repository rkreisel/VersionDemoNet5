namespace VersionAPI.test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VersionAPI.Libs;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Unit Tests: Library
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnitTestLibs
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
        /// Libs.AssemblyInfoHelper
        /// </summary>
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Libs")]
        public void Test_Libs_AssembyInfoHelper()
        {
            var assembly = typeof(UnitTestLibs).Assembly;
            foreach (var attribute in assembly.GetCustomAttributesData())
            {
                var name = attribute.AttributeType.Name;
                if (!attribute.TryParse(out var value))
                {
                    value = string.Empty;
                }
                else
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(value), $"Attribute Name Empty: {name}");
                }
                _testcontext.WriteLine($"{name}, {value}");
            }
        }

    }
}
