using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _dbContext;

        public DevEventsController(DevEventsDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvent = _dbContext.DevEvents.Where(d => !d.isDeleted).ToList();

            return Ok(devEvent);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _dbContext.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        [HttpPost]
        public IActionResult Post(DevEvents devEvent)
        {
            _dbContext.DevEvents.Add(devEvent);

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvents input)
        {
            var devEvent = _dbContext.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _dbContext.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            return Ok();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _dbContext.DevEvents.SingleOrDefault(d => d.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Speakers.Add(speaker);

            return NoContent();
        }
    }
}
