using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VersionAPI.Controllers
{
    /// <summary>
    /// Dummy V2
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("/v2/dummy")]
    public class DummyControllerV2 : ControllerBase
    {
        private readonly ILogger<DummyControllerV2> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyControllerV2"/> class.
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger</param>
        public DummyControllerV2(ILogger<DummyControllerV2> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Get Dummy
        /// </summary>
        /// <returns>String</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DummyGet()
        {
            this._logger.LogInformation("DummyGet()");
            return this.Ok("dummy");
        }

        /// <summary>
        /// Dummy List
        /// </summary>
        /// <returns>List of string</returns>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DummyList()
        {
            this._logger.LogInformation("DummyList()");
            return this.Ok(new string[] { "A", "B", "C" });
        }

        /// <summary>
        /// Echo
        /// </summary>
        /// <param name="text">Text to echo</param>
        /// <returns>Echo of text</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DummyPost(string text)
        {
            this._logger.LogInformation($"DummyPost({text})");
            return this.Ok(text);
        }
    }
}
