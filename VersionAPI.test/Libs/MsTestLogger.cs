using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

// See: https://gist.githubusercontent.com/BlitzkriegSoftware/660f026bea55426a7966ae0a5ad7d981/raw/5bfbbbab578a6181da4f9f7517780e3b1a969d67/MsTestLogger.cs

namespace VersionAPI.test.Libs
{
    /// <summary>
    /// MsTestLogger<typeparamref name="T"/> for ILogger
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ExcludeFromCodeCoverage]
    public class MsTestLogger<T> : ILogger<T>, IDisposable
    {
        private readonly TestContext _testContext;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="testContext">TestContext</param>
        public MsTestLogger(TestContext testContext)
        {
            this._testContext = testContext;
        }
        /// <summary>
        /// Log
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">LogLevel</param>
        /// <param name="eventId">EventId</param>
        /// <param name="state">TState</param>
        /// <param name="exception">Exception</param>
        /// <param name="formatter">Func</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            this._testContext.WriteLine("{0}: {1}\n{2}", logLevel, state.ToString(), (exception == null) ? string.Empty : exception.ToString());
        }

        /// <summary>
        /// Is Enanbled
        /// </summary>
        /// <param name="logLevel">(ignored)</param>
        /// <returns>True</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Begin Scope
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state">TState</param>
        /// <returns>this</returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        /// <summary>
        /// Dispose: Placeholder
        /// </summary>
        public void Dispose()
        {
            // do nothing
        }
    }
}
