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
    public class GarmentController : ControllerBase
    {
        private readonly IGarmentService _garmentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="garmentService">DI injected garmentService</param>
        /// <param name="mapper">DI injected automapper</param>
        public GarmentController(IGarmentService garmentService)
        {
            _garmentService = garmentService;
        }

        // GET: api/Garment
        /// <summary>
        /// Get a list of all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<StyleMateGarment> GetGarment()
        {
            var Garments = _garmentService.GetStyleMateGarment();
            return Garments;
        }
    }
}