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
        public IActionResult GetAge(string pesel)
        {
            //Exception
            //if (pesel.Length != 11)
            //{
            //    throw new HttpRequestException("Podany PESEL ma nieprawidłową długość");
            //}

            return Ok(_peselService.Age(pesel));
        }

        //Usługa 2.
        [HttpGet("GetPromotion/{pesel}", Name = "Promocja")]
        public IActionResult GetPromotion(string pesel)
        {
            return Ok(_peselService.Promotion(pesel));
        }

        //Usługa 3.
        [HttpGet("GetWishes/{pesel}/{name}/{surname}", Name = "Życzenia")]
        public IActionResult GetWishes(string pesel, string name, string surname)
        {
            return Ok(_peselService.Wishes(pesel, name, surname));
        }
    }
}
