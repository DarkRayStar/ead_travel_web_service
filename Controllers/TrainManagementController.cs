using Microsoft.AspNetCore.Mvc;
using TransportManagmentSystemAPI.Models;
using TransportManagmentSystemAPI.Services;

// This controller manages train schedules.
namespace TransportManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainManagementController : ControllerBase
    {
        private readonly TrainManagementService trainManagementService;
        public TrainManagementController(TrainManagementService _trainManagementService)
        {
            trainManagementService = _trainManagementService;
        }

        // POST - Insert shedule for train
        [HttpPost]
        public ActionResult Post(TrainManagement train)
        {
            var createdTrain = trainManagementService.CreateNewTrainSchedule(train);
            if (createdTrain != null)
            {
                return Ok(createdTrain);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET - All train details
        [HttpGet]
        public ActionResult Get()
        {
           return Ok(trainManagementService.getAllAvailableTrainSchedules());
        }

        // GET - train details by ID
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // PUT - update train shedule by id
        [HttpPut("{id}")]
        public ActionResult Put(string id, Schedule schedule)
        {
           return Ok(trainManagementService.AddNewTrainScheduleForAlreadyExisitTrain(id, schedule)); 
        }

        // DELETE - created shedule for train
        [HttpDelete("{id}")]
        public ActionResult Delete(string id , TrainManagement train)
        {
            return Ok(trainManagementService.cancelAvailableTrainSchedule(id,train));
        }
    }
}
