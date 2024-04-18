using Microsoft.AspNetCore.Mvc;

namespace Z5.Animals.Controllers
{
    [ApiController]
    [Route("/api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;
        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllAnimals([FromQuery] string orderBy)
        {
            var animals = _animalService.GetAllAnimals(orderBy);
            return Ok(animals);
        }

        [HttpPost("")]
        public IActionResult CreateAnimal([FromBody] AnimalDTOs dto)
        {
            var success = _animalService.AddAnimal(dto);
            return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
        }


        [HttpPut("")]
        public IActionResult EditAnimal([FromBody] AnimalDTOs dto)
        {
            var success = _animalService.EditAnimal(dto);
            return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
        }

        [HttpDelete("")]
        public IActionResult DeleteAnimal([FromBody] AnimalDTOs dto)
        {
            var success = _animalService.DeleteAnimal(dto);
            return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
        }
    }
}
