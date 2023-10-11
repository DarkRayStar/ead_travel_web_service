using TransportManagmentSystemAPI.Models;

namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IScheduleService
    {
        Schedule AddNewScheduleForExisitingTrain(string trainId ,Schedule schedule);
        Schedule UpdateSchedule(string id , Schedule schedule);

    }
}
