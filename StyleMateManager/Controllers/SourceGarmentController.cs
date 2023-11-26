using Microsoft.AspNetCore.Mvc;
using StyleMate.Data.EntityModels;
using StyleMate.Service.Services;

namespace StyleMate.API.Controllers
{
    /// <summary>
    /// Garment controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SourceGarmentController : ControllerBase
    {
        private readonly ISourceGarmentService _sourceGarmentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sourceGarmentService">DI injected garmentService</param>
        /// <param name="mapper">DI injected automapper</param>
        public SourceGarmentController(ISourceGarmentService sourceGarmentService)
        {
            _sourceGarmentService = sourceGarmentService;
        }

        // POST: api/Garment
        /// <summary>
        /// Get a list of all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SyncGarments()
        {
            var result = await _sourceGarmentService.SyncGarments();
            return result;
        }
    }
}