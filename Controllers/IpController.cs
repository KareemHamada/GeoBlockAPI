using GeoBlockAPI.Models;
using GeoBlockAPI.Repositories;
using GeoBlockAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoBlockAPI.Controllers
{
    [ApiController]
    [Route("api/ip")]
    public class IpController : ControllerBase
    {
        private readonly GeoService _geoService;
        private readonly InMemoryRepository _repo;

        public IpController(GeoService geoService, InMemoryRepository repo)
        {
            _geoService = geoService;
            _repo = repo;
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup([FromQuery] string? ipAddress = null)
        {
            ipAddress ??= HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrWhiteSpace(ipAddress))
                return BadRequest("Invalid IP address.");

            var info = await _geoService.GetIpInfoAsync(ipAddress);
            if (info == null)
                return BadRequest("Geo service unavailable.");

            return Ok(info);
        }

        [HttpGet("check-block")]
        public async Task<IActionResult> CheckBlock()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Fallback for localhost testing
            if (ip == "::1" || ip == "127.0.0.1")
                ip = "8.8.8.8";

            if (string.IsNullOrWhiteSpace(ip))
                return BadRequest("Cannot get client IP.");

            var info = await _geoService.GetIpInfoAsync(ip);
            if (info == null)
                return BadRequest("Geo service unavailable.");

            bool isBlocked = _repo.BlockedCountries.ContainsKey(info.CountryCode.ToUpper());

            _repo.AddLog(new BlockedAttemptLog
            {
                IpAddress = ip,
                CountryCode = info.CountryCode,
                IsBlocked = isBlocked,
                UserAgent = Request.Headers["User-Agent"].ToString()
            });

            return Ok(new
            {
                ip,
                info.CountryCode,
                info.CountryName,
                info.Isp,
                isBlocked
            });
        }
    }
}
