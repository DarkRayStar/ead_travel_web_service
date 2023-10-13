using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

// This interface defines methods for managing reservations.
namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IReservationManagement
    {
        // Method to create a new reservation.
        Dictionary<int, string> CreateReservation (ReservationManagement reservation);
        // Method to display all reservations for a traveler.
        List<ReservationManagement> DisplayAllReservation(string travallerId);
        // Method to cancel a reservation.
        Dictionary<int, string> CancelledReservation(string id, ReservationManagement reservation);

    }
}
