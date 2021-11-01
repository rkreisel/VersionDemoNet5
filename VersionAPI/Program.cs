using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VersionAPI.Libs;
using System.Diagnostics.CodeAnalysis;

namespace VersionAPI
{
    /// <summary>
    /// Program Start
    /// </summary>
    [ExcludeFromCodeCoverage]
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    public class Program
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args">Command Line Arguments</param>
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Host created.");
            host.Run();
        }

        /// <summary>
        /// Host builder
        /// </summary>
        /// <param name="args">Command Line Arguments</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        #region "Assembly Metadata"

        private static Models.AssemblyVersionMetadata _assemblyVersionMetadata;

        /// <summary>
        /// Gets semantic Version, etc from Assembly Metadata
        /// </summary>
        public static Models.AssemblyVersionMetadata ProgramMetadata
        {
            get
            {
                if (_assemblyVersionMetadata == null)
                {
                    _assemblyVersionMetadata = new Models.AssemblyVersionMetadata();
                    var assembly = typeof(Program).Assembly;
                    foreach (var attribute in assembly.GetCustomAttributesData())
                    {
                        if (!attribute.TryParse(out var value))
                        {
                            value = string.Empty;
                        }

                        var name = attribute.AttributeType.Name;
                        System.Diagnostics.Trace.WriteLine($"{name}, {value}");
                        _assemblyVersionMetadata.PropertySet(name, value);
                    }
                }

                return _assemblyVersionMetadata;
            }
        }

        #endregion
    }
}
