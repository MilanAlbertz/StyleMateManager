using Microsoft.AspNetCore.Mvc;
using StyleMate.Data.EntityModels;
using System;

namespace StyleMate.Service.Services
{
    /// <summary>
    /// Interface defining the garment service
    /// </summary>
    public interface ISourceGarmentService
    {
        /// <summary>
        /// Get a list with all garments
        /// </summary>
        /// <returns>The list of garments</returns>
        public Task<IActionResult> SyncGarments();
    }
}