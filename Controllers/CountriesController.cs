using GeoBlockAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GeoBlockAPI.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly InMemoryRepository _repo;

        public CountriesController(InMemoryRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("block")]
        public IActionResult BlockCountry([FromBody] string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                return BadRequest("Country code is required.");

            if (_repo.BlockedCountries.ContainsKey(countryCode.ToUpper()))
                return Conflict("Country already blocked.");

            _repo.AddCountry(countryCode);
            return Ok($"{countryCode} blocked successfully.");
        }

        [HttpDelete("block/{countryCode}")]
        public IActionResult UnblockCountry(string countryCode)
        {
            if (!_repo.RemoveCountry(countryCode))
                return NotFound("Country not found.");

            return Ok($"{countryCode} unblocked successfully.");
        }

        [HttpGet("blocked")]
        public IActionResult GetBlockedCountries()
        {
            return Ok(_repo.GetBlockedCountries());
        }
    }
}
