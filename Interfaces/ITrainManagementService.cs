using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

namespace TransportManagmentSystemAPI.Interfaces
{
    public interface ITrainManagementService
    {
        TrainManagement CreateNewTrainSchedule(TrainManagement train);
        List<TrainManagement> getAllAvailableTrainSchedules();
        string cancelAvailableTrainSchedule(string id , TrainManagement train);

        TrainManagement AddNewTrainScheduleForAlreadyExisitTrain(string trainId, Schedule schedule);

    }
}
