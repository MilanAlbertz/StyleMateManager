using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StyleMate.Data.EntityModels;
using StyleMate.Service.Services;
using StyleMateManager.API.Migrations;

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
        /// <param name="garmentService">DI injected peopleService</param>
        /// <param name="mapper">DI injected automapper</param>
        public GarmentController(IGarmentService garmentService)
        {
            _garmentService = garmentService;
        }

        // GET: api/People
        /// <summary>
        /// Get a list of all people
        /// </summary>
        /// <returns>The list of people</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public List<StyleMateGarment> GetGarment()
        {
            var Garments = _garmentService.GetStyleMateGarment();
            return Garments;
        }
    }
}