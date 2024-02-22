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
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<StyleMateGarment> GetGarment()
        {
            var Garments = _garmentService.GetStyleMateGarment();
            return Garments;
        }

        // GET: api/LikedGarments
        /// <summary>
        /// Get a list of all liked garments
        /// </summary>
        /// <returns>The list of liked garments</returns>
        [HttpGet("likedgarments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<StyleMateGarment> GetLikedGarments([FromQuery] string userId)
        {
            var Garments = _garmentService.GetLikedGarments(userId);
            return Garments;
        }

        // GET: api/GetSomeRandomGarments
        /// <summary>
        /// <summary>
        /// Get a list of all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        [HttpGet("random10")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<StyleMateGarment> Get10Garments([FromQuery] string type = "Everything", [FromQuery] string gender = "")
        {
            var Garments = _garmentService.Get10StyleMateGarment(type, gender);
            return Garments;
        }

        // GET: api/GetSomeRandomGarments
        /// <summary>
        /// <summary>
        /// Get a list of all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        [HttpPost("StoreLikedGarmentToUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task StoreLikedGarmentToUser([FromQuery] int garmentId, [FromQuery] string userId)
        {
            _garmentService.StoreLikedGarmentToUser(garmentId, userId);
            return Task.CompletedTask;
        }
    }
}