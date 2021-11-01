using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace VersionAPI.test.Libs
{
    /// <summary>
    /// https://gist.githubusercontent.com/BlitzkriegSoftware/6fab86323ede3a424281c8a8e339c606/raw/1e737ad9457fbebc2725929ee8dbe25bbb865751/SerializationHelper.cs
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SerializationHelper
    {

        /// <summary>
        /// Tests that a model serializes correctly
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="thing1">Instance of T to test</param>
        /// <param name="testContext">XUnit Test Output</param>
        public static void AssertSerialization<T>(TestContext testContext, T thing1)
        {
            var json = JsonConvert.SerializeObject(thing1);

            testContext?.WriteLine("{0} -> {1}", thing1.GetType().Name, json);

            T thing2 = JsonConvert.DeserializeObject<T>(json);

            var t1 = thing1.GetType()
                .GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(thing1, null));

            var t2 = thing2.GetType()
                .GetProperties()
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p.GetValue(thing2, null));

            foreach (var key in t1.Keys)
            {
                var type = thing1.GetType();
                var mem = type.GetMember(key).First();
                if (!Attribute.IsDefined(mem, typeof(JsonIgnoreAttribute)))
                {
                    if (t1[key] != null)
                    {
                        Assert.AreEqual(t1[key].ToString(), t2[key].ToString());
                    }
                }
            }
        }
    }
}