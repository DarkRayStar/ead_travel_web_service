using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TransportManagmentSystemAPI.DBconfig;
using TransportManagmentSystemAPI.Interfaces;
using TransportManagmentSystemAPI.Models;

// This class provides reservation management services.
namespace TransportManagmentSystemAPI.Services
{
    public class ReservationManagementService : IReservationManagement
    {
        private readonly IMongoCollection<ReservationManagement> _reservationList;
        private readonly IMongoCollection<TrainManagement> _trainList;

        public ReservationManagementService(IDatabaseSettings _databaseSettings, IScheam _scheam)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reservationList = database.GetCollection<ReservationManagement>(
                _scheam.ReservationScheam
            );
            _trainList = database.GetCollection<TrainManagement>(_scheam.TrainScheam);
        }

        // Method to display all reservations for a traveler.
        public List<ReservationManagement> DisplayAllReservation(string travallerId)
        {
            var listofBookings =
                travallerId != null
                    ? _reservationList.Find(res => res.ReferenceId == travallerId).ToList()
                    : _reservationList.Find(res => true).ToList();
            int index = 0;
            foreach (ReservationManagement res in listofBookings)
            {
                index = index + 1;
                TrainManagement gottrain = _trainList
                    .Find(tra => tra.Id == res.Train)
                    .ToList()
                    .FirstOrDefault();
                listofBookings[index - 1].BookedTrain = gottrain;
            }
            if (listofBookings.Count > 0)
            {
                return listofBookings;
            }
            else
            {
                return null;
            }
        }

        // Method to create a new reservation.
        public Dictionary<int, string> CreateReservation(ReservationManagement reservation)
        {
            Dictionary<int, string> returnCode = new Dictionary<int, string>();
            var DateToday = DateTime.Now;
            var ReservationDate = reservation.ReservationDate;

            TimeSpan timeDifferenceWhenReserve = ReservationDate - DateToday;

            if (reservation.Id != null)
            {
                ReservationManagement existingReservationforUpdate = _reservationList
                    .Find(exres => exres.Id == reservation.Id)
                    .FirstOrDefault();
                TimeSpan timeDiff = existingReservationforUpdate.ReservationDate - DateToday;

                if (timeDiff.TotalDays < 5)
                {
                    returnCode.Add(
                        400,
                        "Your reservation is confirmed and cannot be updated or canceled."
                    );
                    return returnCode;
                }
                else if (timeDifferenceWhenReserve.TotalDays < 0)
                {
                    returnCode.Add(400, "Invalid reservation date. Please choose a valid date.");
                    return returnCode;
                }
                else
                {
                    reservation.BookingCreatedDate = DateToday;
                    _reservationList.ReplaceOne(res => res.Id == reservation.Id, reservation);
                    returnCode.Add(200, "Reservation successfully updated.  " + reservation.Id);
                    return returnCode;
                }
            }
            else
            {
                if (
                    timeDifferenceWhenReserve.TotalDays >= 30
                    || timeDifferenceWhenReserve.TotalDays < 0
                )
                {
                    returnCode.Add(
                        400,
                        "Reservations must be made within 30 days from the booking date."
                    );
                    return returnCode;
                }
                else
                {
                    var existingReservationB = _reservationList
                        .Find(res => res.ReferenceId == reservation.ReferenceId && !res.IsCancelled)
                        .ToList();
                    var validCount = 0;
                    foreach (ReservationManagement ex in existingReservationB)
                    {
                        TimeSpan timediffEx = ex.ReservationDate - DateToday;
                        if (timediffEx.TotalDays > 0)
                        {
                            validCount++;
                        }
                    }

                    if (validCount >= 4)
                    {
                        returnCode.Add(400, "Maximum of 4 reservations allowed per user.");
                        return returnCode;
                    }
                    else
                    {
                        reservation.BookingCreatedDate = DateToday;
                        _reservationList.InsertOne(reservation);
                        returnCode.Add(200, "Reservation successfully created. " + reservation.Id);
                        return returnCode;
                    }
                }
            }
        }

        // Method to cancel a reservation.
        public Dictionary<int, string> CancelledReservation(
            string id,
            ReservationManagement reservation
        )
        {
            Dictionary<int, string> cancellingStat = new Dictionary<int, string>();
            if (reservation != null)
            {
                ReservationManagement existingReservation = _reservationList
                    .Find(exres => exres.Id == id)
                    .FirstOrDefault();
                TimeSpan timeDiff = existingReservation.ReservationDate - DateTime.Now;

                if (timeDiff.TotalDays >= 5)
                {
                    var Cancelled = Builders<ReservationManagement>.Update.Set(
                        res => res.IsCancelled,
                        reservation.IsCancelled
                    );

                    var updatedProfile = _reservationList.UpdateOne(
                        reser => reser.Id == id,
                        Cancelled
                    );
                    cancellingStat.Add(
                        100,
                        "Reservation has been canceled. reference ID "
                            + existingReservation.ReferenceId
                    );
                    return cancellingStat;
                }
                else
                {
                    cancellingStat.Add(
                        500,
                        "Your reservation is confirmed and cannot be canceled."
                    );
                    return cancellingStat;
                }
            }
            else
            {
                return null;
            }
        }

        // Method to delete a reservation.
        public Dictionary<int, string> DeleteReservation(string id)
        {
            Dictionary<int, string> deletionStatus = new Dictionary<int, string>();

            ReservationManagement existingReservation = _reservationList
                .Find(exres => exres.Id == id)
                .FirstOrDefault();
            if (existingReservation != null)
            {
                // Check if the reservation can be deleted (at least 5 days before the reservation date)
                TimeSpan timeDiff = existingReservation.ReservationDate - DateTime.Now;
                if (timeDiff.TotalDays >= 5)
                {
                    try
                    {
                        var deletionResult = _reservationList.DeleteOne(reser => reser.Id == id);
                        deletionStatus.Add(200, "Reservation deleted successfully");
                    }
                    catch (Exception)
                    {
                        deletionStatus.Add(500, "Failed to delete");
                    }
                }
                else
                {
                    deletionStatus.Add(
                        400,
                        "Cannot delete the reservation as it is confirmed or less than 5 days before the reservation date."
                    );
                }
            }
            else
            {
                deletionStatus.Add(404, "Reservation not found");
            }

            return deletionStatus;
        }
    }
}
