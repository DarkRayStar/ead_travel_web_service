using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;
using TransportManagmentSystemAPI.Services;
using System;

// This controller manages reservations.
namespace TransportManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationManagementController : ControllerBase
    {
        private readonly ReservationManagementService _reservationService;

        public ReservationManagementController(ReservationManagementService reservationService)
        {
            _reservationService = reservationService;
        }

        // Get a list of reservations by traveler ID.
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            List<ReservationManagement> reservationList = _reservationService.DisplayAllReservation(
                id
            );
            if (reservationList != null)
            {
                return Ok(reservationList);
            }
            else
            {
                return NotFound();
            }
        }

        // Create a new reservation.
        [HttpPost]
        public ActionResult Post(ReservationManagement reservation)
        {
            var resv = _reservationService.CreateReservation(reservation);

            if (resv.ContainsKey(100))
            {
                return Ok(resv[100]);
            }
            else if (resv.ContainsKey(200))
            {
                return Ok(resv[200]);
            }
            else
            {
                return BadRequest(resv[400]);
            }
        }

        // Get a list of all reservations.
        [HttpGet]
        public ActionResult Get()
        {
            List<ReservationManagement> reservationList = _reservationService.DisplayAllReservation(
                null
            );
            if (reservationList != null)
            {
                return Ok(reservationList);
            }
            else
            {
                return NotFound();
            }
        }

        // Cancel a reservation.
        [HttpPut("{id}")]
        public ActionResult Put(string id, ReservationManagement reservation)
        {
            var resv = _reservationService.CancelledReservation(id, reservation);
            if (resv.ContainsKey(100))
            {
                return Ok(resv[100]);
            }
            else
            {
                return BadRequest(resv[500]);
            }
        }

        // Delete a reservation.
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var deletionResult = _reservationService.DeleteReservation(id);

            if (deletionResult.ContainsKey(200))
            {
                return Ok("Reservation deleted successfully");
            }
            else if (deletionResult.ContainsKey(404))
            {
                return NotFound(deletionResult[404]);
            }
            else if (deletionResult.ContainsKey(400))
            {
                return NotFound(deletionResult[400]);
            }
            else
            {
                return BadRequest("Failed to delete reservation");
            }
        }
    }
}
