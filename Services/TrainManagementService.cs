using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportManagmentSystemAPI.DBconfig;
using TransportManagmentSystemAPI.Interfaces;
using TransportManagmentSystemAPI.Models;

// Train Service managment 
namespace TransportManagmentSystemAPI.Services
{
    public class TrainManagementService : ITrainManagementService
    {
        private readonly IMongoCollection<TrainManagement> _trainList;
        private readonly IMongoCollection<Schedule> _schedueList;
        private readonly IMongoCollection<ReservationManagement> _reservationList;
        public TrainManagementService(IDatabaseSettings _databaseSettings, IScheam _scheam)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _trainList = database.GetCollection<TrainManagement>(_scheam.TrainScheam);
            _schedueList = database.GetCollection<Schedule>(_scheam.ScheduleScheam);
            _reservationList = database.GetCollection<ReservationManagement>(_scheam.ReservationScheam);
        }

        // create new Train shedule
        public TrainManagement CreateNewTrainSchedule(TrainManagement train)
        {
            var ListoftheSchedules = train.ScheduleList;
            if (train.Id == null)
            {
                if (ListoftheSchedules != null && ListoftheSchedules.Count > 0)
                {
                    foreach (Schedule schedule in ListoftheSchedules)
                    {
                         _schedueList.InsertOne(schedule);
                    }
                }
                _trainList.InsertOne(train);
                return train;
            }
            else
            {
                if (ListoftheSchedules != null && ListoftheSchedules.Count > 0) {
                    foreach (Schedule schedule in ListoftheSchedules)
                    {
                        _schedueList.ReplaceOne(sch => sch.Id == schedule.Id, schedule);
                    }
                }
                if (train.IsCancelled)
                {
                    var trinReservationCount = _reservationList.Find(res => res.Id == train.Id).ToList().Count;
                    if (trinReservationCount > 0)
                    {
                        return null;
                    }
                }
                _trainList.ReplaceOne(tra => tra.Id == train.Id, train);
                return train;
            }
        }

        // add new shedule for exsiting train
        public TrainManagement AddNewTrainScheduleForAlreadyExisitTrain(string trainId, Schedule schedule)
        {
            var Selectedtrain = _trainList.Find(tra => tra.Id == trainId).ToList().FirstOrDefault();

            if (Selectedtrain != null)
            {
                var extingList = Selectedtrain.ScheduleList;
                _schedueList.InsertOne(schedule);
                extingList.Add(schedule);
                _trainList.ReplaceOne(tra => tra.Id == trainId, Selectedtrain);

                return Selectedtrain;
            }
            else
            {
                return null;
            }
        }

        // retriwe all sheduled trains (Available trains)
        public List<TrainManagement> getAllAvailableTrainSchedules()
        {
           return _trainList.Find(tra => tra.IsActive).ToList();
        }

        // cancel the shedule
        public string cancelAvailableTrainSchedule(string id , TrainManagement train)
        {
            var trinReservationCount = _reservationList.Find(res => res.Train == id).ToList().Count;
            if (trinReservationCount > 0)
            {
                return "The train has already been reserved.";
            }
            else 
            {
                train.IsCancelled = true;
                _trainList.ReplaceOne(tra => tra.Id == train.Id, train);
                return "The train has been canceled.";
            }
             
        }

        
    }
}
