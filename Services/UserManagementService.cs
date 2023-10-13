using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagmentSystemAPI.DBconfig;
using TransportManagmentSystemAPI.Interfaces;
using TransportManagmentSystemAPI.Models;

// login Service Management
namespace TransportManagmentSystemAPI.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IMongoCollection<UserManagement> _userList;
        private readonly IMongoCollection<TravallerManagement> _travalerProfileList;

        // Constructor for UserManagementService.
        public UserManagementService(IDatabaseSettings _databaseSettings, IScheam _scheam)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _travalerProfileList = database.GetCollection<TravallerManagement>(_scheam.TravellerScheama);
            _userList = database.GetCollection<UserManagement>(_scheam.UsersScheama);
        }

        // Method for user login management.
        public UserManagement UserLoginMangement(UserManagement user)
        {
            if (user.Nic != null && user.Password != null)
            {
               var profileActiveOrExist = _travalerProfileList.Find(pro => pro.Nic == user.Nic && pro.AccStatus).FirstOrDefault();
                if (profileActiveOrExist != null)
                {
                  var validatedUser =   _userList.Find(us => us.Nic == user.Nic && us.Password == user.Password).FirstOrDefault();
                    return validatedUser != null ? validatedUser : null;
                }
                else 
                {
                    return null;
                }
            } 
            else 
            {
                throw new NullReferenceException("Please Enter your Login Credentials!");
            }
        }
    }
}
