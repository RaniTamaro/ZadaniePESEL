using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZadaniePESEL.Services;

namespace ZadaniePESEL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeselController : ControllerBase
    {
        public readonly IPeselService _peselService;

        public PeselController(IPeselService peselService)
        {
            _peselService = peselService;
        }

        //Usługa 1.
        [HttpGet("GetAge/{pesel}", Name = "Wiek")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult GetAge(string pesel)
        {
            if (pesel is null || pesel == string.Empty)
            {
                return NotFound();
            }

            if (!_peselService.PeselValidation(pesel))
            {
                return BadRequest();
            }

            return Ok(_peselService.Age(pesel));
        }

        //Usługa 2.
        [HttpGet("GetPromotion/{pesel}", Name = "Promocja")]
        public IActionResult GetPromotion(string pesel)
        {
            if (pesel is null || pesel == string.Empty)
            {
                return NotFound();
            }

            if (!_peselService.PeselValidation(pesel))
            {
                return BadRequest();
            }

            return Ok(_peselService.Promotion(pesel));
        }

        //Usługa 3.
        [HttpGet("GetWishes/{pesel}/{name}/{surname}", Name = "Życzenia")]
        public IActionResult GetWishes(string pesel, string name, string surname)
        {
            if (pesel is null || pesel == string.Empty)
            {
                return NotFound();
            }

            if (!_peselService.PeselValidation(pesel))
            {
                return BadRequest(ModelState);
            }

            return Ok(_peselService.Wishes(pesel, name, surname));
        }
    }
}
