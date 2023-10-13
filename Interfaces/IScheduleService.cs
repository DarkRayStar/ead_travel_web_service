using TransportManagmentSystemAPI.Models;

// This interface defines methods for managing schedules.

namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IScheduleService
    {
        // Method to add a new schedule for an existing train.
        Schedule AddNewScheduleForExisitingTrain(string trainId ,Schedule schedule);
        // Method to update a schedule.
        Schedule UpdateSchedule(string id , Schedule schedule);

    }
}
