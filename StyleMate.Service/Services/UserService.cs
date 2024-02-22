using StyleMate.Data;
using StyleMate.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace StyleMate.Service.Services
{
    public class UserService : IUserService
    {
        private readonly StyleMateDataContext _dbContext;

        public UserService(StyleMateDataContext context)
        {
            _dbContext = context;
        }

        public Task RegisterUser(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}