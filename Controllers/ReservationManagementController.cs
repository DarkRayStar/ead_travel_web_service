using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;
using TransportManagmentSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            List<ReservationManagement> reservationList = _reservationService.DisplayAllReservation(id);
            if (reservationList != null)
            {
                return Ok(reservationList);
            }
            else
            {
                return NotFound();
            }
        }


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
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<ReservationManagement> reservationList = _reservationService.DisplayAllReservation(null);
            if (reservationList != null)
            {
                return Ok(reservationList);
            }
            else
            {
                return NotFound();
            }
        }


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
                return NotFound("Reservation not found");
            }
            else
            {
                return BadRequest("Failed to delete reservation");
            }
        }
    }
}
