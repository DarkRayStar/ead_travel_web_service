using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagmentSystemAPI.DBconfig;
using TransportManagmentSystemAPI.Models;

// Travaller Account managment 
namespace TransportManagmentSystemAPI.Services
{
    public class TravallerManagementService : ITravallerManagementService
    {
        private readonly IMongoCollection<TravallerManagement> _travalerProfileList;
        private readonly IMongoCollection<UserManagement> _userList;
        private readonly IMongoDatabase _database;
        public TravallerManagementService(IDatabaseSettings _databaseSettings,IScheam _scheam)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _travalerProfileList = database.GetCollection<TravallerManagement>(_scheam.TravellerScheama);
            _userList = database.GetCollection<UserManagement>(_scheam.UsersScheama);
        }

        // update and create the traveler account
        public TravallerManagement CreateAndUpdateTravellerAccount(TravallerManagement travallerProfile)
        {
            try
            {
                if (travallerProfile.Id != null)
                {
                    // updating the traveler account
                    var update = Builders<TravallerManagement>.Update.
                        Set(upf => upf.FirstName, travallerProfile.FirstName)
                        .Set(upf => upf.LastName, travallerProfile.LastName)
                        .Set(upf => upf.PhoneNumber, travallerProfile.PhoneNumber);

                    var updatedProfile = _travalerProfileList.UpdateOne(trav => trav.Id == travallerProfile.Id, update);

                    if (travallerProfile?.UserInfo != null && travallerProfile.UserInfo.Password != null)
                    {
                        var updatePassword = Builders<UserManagement>.Update.
                            Set(upf => upf.Password, travallerProfile.UserInfo.Password);
                        var passwordReset = _userList.UpdateOne(up => up.Nic == travallerProfile.Nic, updatePassword);
                    }
                    
                    return travallerProfile;
                }
                else
                {
                    //creating the traveller profile
                    var uniqueCounts = _travalerProfileList.Find(trv => trv.Nic == travallerProfile.Nic).ToList().Count;
                    if (uniqueCounts == 0)
                    {
                        travallerProfile.CreatedDate = DateTime.Now;
                        travallerProfile.UserInfo.Nic = travallerProfile.Nic;
                        _userList.InsertOne(travallerProfile.UserInfo);
                        _travalerProfileList.InsertOne(travallerProfile);
                        return travallerProfile;
                    }
                    else
                    {
                        return null;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred: ERR_LOGGED!" + e.ToString());
            }
            
        }

        // Get traveller details using NIC
        public TravallerManagement GetTravallerAccountDetailsByNic(string _Nic)
        {
            var profile = _travalerProfileList.Find(pro => pro.Nic == _Nic).ToList().FirstOrDefault();
            if (profile != null)
            {
                return profile;
            }
            else
            {
                return null;
            }
        }

        // Get All Activated accouts
        public List<TravallerManagement> DisplayAllActiveAccounts(bool isActive)
        {
            try
            {
                var profileList = _travalerProfileList.Find(trav => trav.AccStatus == isActive).ToList();

                List<TravallerManagement> secureProfileList = profileList.Select(item =>
                {
                    item.UserInfo = null;
                    return item;
                }
                ).ToList();

                return secureProfileList.Count > 0 ? secureProfileList : null;
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred: ERR_LOGGED!" + e.ToString());
            }
        }

        // delete traveller account
        public String DeletedTravellerAccount(String _Nic)
        {
            try
            {
                _travalerProfileList.DeleteOne(trv => trv.Nic == _Nic);
                return _Nic;

            }
            catch (Exception e)
            {
                throw new Exception("An error occurred: ERR_LOGGED!" + e.ToString());
            }
        }

        // Traveler profile Activation and Deactivation
        public TravallerManagement ManageActivationTravellerAccountDetails(string nic , TravallerManagement travallerProfile)
        {
            try
            {
                if (nic != null)
                {
                    var updatedStatus = Builders<TravallerManagement>.Update.
                           Set(upf => upf.AccStatus, travallerProfile.AccStatus);
                    _travalerProfileList.UpdateOne(trav => trav.Nic == nic, updatedStatus);
                    return travallerProfile;
                }
                else 
                {
                    return null;
                }
               
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred: ERR_LOGGED!" + e.ToString());
            }
        }

        
    }
}
