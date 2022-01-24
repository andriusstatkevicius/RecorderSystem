using Microsoft.AspNetCore.Mvc;

namespace RecorderSystem.Controllers
{
    [ApiController]
    [Route("api/cities")] // only sufficient to define once and not on each action
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new { id = 1, Name="New York City"},
                    new { id = 2, Name = "Anwerp"}
                });
        }
    }
}
