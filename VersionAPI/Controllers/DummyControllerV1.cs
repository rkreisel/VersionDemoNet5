using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VersionAPI.Controllers
{
    /// <summary>
    /// V1 Dummy
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("/v1/dummy")]
    public class DummyControllerV1 : ControllerBase
    {
        private readonly ILogger<DummyControllerV1> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyControllerV1"/> class.
        /// CTOR
        /// </summary>
        /// <param name="logger">ILogger (injected)</param>
        public DummyControllerV1(ILogger<DummyControllerV1> logger)
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
