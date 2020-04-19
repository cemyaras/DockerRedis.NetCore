using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DockerRedis.NetCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cacheKey = "cacheTime";
            var cacheTime = _cache.GetString(cacheKey);

            if (string.IsNullOrEmpty(cacheTime))
            {
                cacheTime = DateTime.UtcNow.ToString();

                var option = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
                };

                await _cache.SetStringAsync(cacheKey, cacheTime, option);
                return Ok("Updated Cache Time : " + cacheTime);
            }

            return Ok("Cache Time : " + cacheTime);
        }
    }
}
