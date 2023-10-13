using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

// This interface defines methods for managing train schedules.
namespace TransportManagmentSystemAPI.Interfaces
{
    public interface ITrainManagementService
    {
        // Method to create a new train schedule.
        TrainManagement CreateNewTrainSchedule(TrainManagement train);
         // Method to get a list of all available train schedules.
        List<TrainManagement> getAllAvailableTrainSchedules();
        // Method to cancel an available train schedule.
        string cancelAvailableTrainSchedule(string id , TrainManagement train);
        // Method to add a new schedule for an already existing train.
        TrainManagement AddNewTrainScheduleForAlreadyExisitTrain(string trainId, Schedule schedule);

    }
}
