using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZadaniePESEL.Services;

namespace ZadaniePESEL.Controllers
{
    /// <summary>
    /// Kontroler obsługujący żądania API.
    /// </summary>
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
        public IActionResult GetAge(string pesel)
        {
            try
            {
                //Walidacja danych
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
            catch
            {
                return Problem();
            }
        }

        //Usługa 2.
        [HttpGet("GetPromotion/{pesel}", Name = "Promocja")]
        public IActionResult GetPromotion(string pesel)
        {
            try
            {
                //Walidacja danych
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
            catch
            {
                return Problem();
            }
        }

        //Usługa 3.
        [HttpGet("GetWishes/{pesel}/{name}/{surname}", Name = "Życzenia")]
        public IActionResult GetWishes(string pesel, string name, string surname)
        {
            try
            {
                //Walidacja danych
                if (pesel is null || pesel == string.Empty)
                {
                    return NotFound();
                }

                if (!_peselService.PeselValidation(pesel))
                {
                    return BadRequest(ModelState);
                }

                if (name is null || name == string.Empty)
                {
                    return NotFound();
                }

                if (surname is null || surname == string.Empty)
                {
                    return NotFound();
                }

                return Ok(_peselService.Wishes(pesel, name, surname));
            }
            catch
            {
                return Problem();
            }
        }
    }
}
