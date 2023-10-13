using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransportManagmentSystemAPI.Models;
using TransportManagmentSystemAPI.Services;

// This controller manages user login and accounts.
namespace TransportManagmentSystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementService _loginservice;
        public UserManagementController(UserManagementService loginService)
        {
            _loginservice = loginService;
        }

        // POST - User login management.
        [HttpPost]
        public ActionResult Post(UserManagement user)
        {
            if (user.Nic != null && user.Password != null)
            {
                var ValdatedAccount = _loginservice.UserLoginMangement(user);
                if (ValdatedAccount != null)
                {
                    return Ok(ValdatedAccount);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else {
               return  BadRequest("Please enter NIC and Password");
            }
            
        }

        // GET - Retrieve user details
        [HttpGet]
        public Task<UserManagement> Get()
        {
            return null;
        }

    }
}
