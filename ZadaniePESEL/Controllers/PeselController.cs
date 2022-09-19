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

        [HttpGet(Name = "Wiek")]
        public IActionResult GetAge(string pesel)
        {
            //Exception
            //if (pesel.Length != 11)
            //{
            //    throw new HttpRequestException("Podany PESEL ma nieprawidłową długość");
            //}

            var birthDate = _peselService.BithDate(pesel);
            var a = 1;
            var b = 1;

            var age = DateTime.Today.Subtract(birthDate);
            var c = 1;
            var d = 1;

            return Ok(age);
        }

        //[HttpGet(Name = "Promocja")]
        //public IActionResult GetPromotion(string pesel)
        //{
            
        //}
    }
}
