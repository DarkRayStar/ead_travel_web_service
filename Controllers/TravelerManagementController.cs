using Microsoft.AspNetCore.Mvc;
using TransportManagmentSystemAPI.Models;
using TransportManagmentSystemAPI.Services;

// This controller manages traveler accounts.
namespace TransportManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerManagementController : ControllerBase
    {
        private readonly TravallerManagementService travallerManagementService;
        public TravelerManagementController(TravallerManagementService _travallerProfile)
        {
            travallerManagementService = _travallerProfile;
        }
        // GET - Get all travellers details
        [HttpGet]
        public ActionResult<TravallerManagement> Get(bool isActive) 
         {
            var activeProfile = travallerManagementService.DisplayAllActiveAccounts(isActive);
            if (activeProfile != null)
            {
                return Ok(activeProfile);
            }
            else 
            {
                return NotFound();
            }
        }

        // POST - Create traveller account
        [HttpPost]
        public ActionResult Post(TravallerManagement _travallerProfile)
        {
            var createdAccount = travallerManagementService.CreateAndUpdateTravellerAccount(_travallerProfile);
            if (createdAccount != null)
            {
                return Ok(createdAccount);
            }
            else
            {
                return Conflict();
            }
        }

        // GET - get traveller detail using ID
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var profile = travallerManagementService.GetTravallerAccountDetailsByNic(id);
            if (profile != null)
            {
                return Ok(profile);
            }
            else 
            {
                return NotFound();
            }
                
        }

        // PUT - Update Traveller details using ID
        [HttpPut("{id}")]
        public ActionResult Put(string id, TravallerManagement _travallerProfile)
        {
            
            var updatedAccount = travallerManagementService.ManageActivationTravellerAccountDetails(id, _travallerProfile);
            if (updatedAccount != null)
            {
                return Ok(updatedAccount);
            }
            else 
            {
                return BadRequest();
            }
        }

        // DELETE - Traveller Account by using ID
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
           var deletedNic = travallerManagementService.DeletedTravellerAccount(id);
            if (deletedNic != null)
            {
                return Ok("Deleted" + deletedNic);
            }
            else 
            {
                return BadRequest();
            }
        }
    }
}
