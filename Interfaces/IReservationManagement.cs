using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IReservationManagement
    {
        Dictionary<int, string> CreateReservation (ReservationManagement reservation);
        List<ReservationManagement> DisplayAllReservation(string travallerId);
        Dictionary<int, string> CancelledReservation(string id, ReservationManagement reservation);

    }
}
