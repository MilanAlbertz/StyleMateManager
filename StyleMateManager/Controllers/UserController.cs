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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="garmentService">DI injected garmentService</param>
        /// <param name="mapper">DI injected automapper</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // POST: api/Garment
        /// <summary>
        /// Register the user
        /// </summary>
        /// <returns>The list of garments</returns>
        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task RegisterUser([FromQuery] string userID)
        {
            _userService.RegisterUser(new Data.EntityModels.User() { Id = userID });
            return Task.CompletedTask;
        }
    }
}