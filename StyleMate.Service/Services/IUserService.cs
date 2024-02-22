using StyleMate.Data.EntityModels;
using System;

namespace StyleMate.Service.Services
{
    /// <summary>
    /// Interface defining the user service
    /// </summary>
    public interface IUserService
    {
        Task RegisterUser(User user);
    }
}