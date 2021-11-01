using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace VersionAPI.Controllers
{
    /// <summary>
    /// Semantic Version
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "common")]
    [Route("/")]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonController"/> class.
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger</param>
        public CommonController(ILogger<CommonController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Get Version
        /// </summary>
        /// <returns>Version String</returns>
        [HttpGet]
        [Produces("text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult VersionGet()
        {
            var version = Program.ProgramMetadata.SemanticVersion;
            this._logger.LogInformation($"VersionGet(): {version}");
            return this.Ok(version);
        }

        /// <summary>
        /// Get Version (long form)
        /// </summary>
        /// <returns>Version String</returns>
        [HttpGet("version")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult VersionDetailsGet()
        {
            this._logger.LogInformation(Program.ProgramMetadata.ToString());
            return this.Ok(Program.ProgramMetadata);
        }

        /// <summary>
        /// Health Check
        /// </summary>
        /// <returns>HealthCheckResult</returns>
        [HttpGet("health")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public HealthCheckResult CheckHealth()
        {

            // TODO: Add your own values
            var info = new Dictionary<string, object>
            {
                { "Product", Program.ProgramMetadata.Product },
                { "SemanticVersion", Program.ProgramMetadata.SemanticVersion },
                { "InformationVersion", Program.ProgramMetadata.InformationVersion }
            };

            // TODO: Make sure your dependencies are healthy or not and set the text
            var deps = new Dictionary<string, object> {
                    { "Dependancy-1", "OK" },
                    { "Dependancy-2", "Timeout" },
                    { "Dependancy-3", "OK" }
            };

            var data = new Dictionary<string, object>
            {
                { "Info" , info },
                { "Dependancies", deps }
            };

            // TODO: Capture any exceptions (if any)
            Exception ex = null;
            
            //Sample
            //var ex = new WebException("Dependancy-2", WebExceptionStatus.Timeout);

            var hcr = (ex == null) ?
                new HealthCheckResult(HealthStatus.Healthy, $"{Program.ProgramMetadata.Product} is healthy", null, data) :
                new HealthCheckResult(HealthStatus.Unhealthy, $"{Program.ProgramMetadata.Product} is unhealthy", ex, data);

            var msg = JsonConvert.SerializeObject(hcr);

            if (ex == null)
            {
                this._logger.LogInformation(msg);
            }
            else
            {
                this._logger.LogWarning(ex, msg);
            }

            return hcr;
        }
    }
}
