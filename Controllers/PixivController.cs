using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PixivController : ControllerBase
    {
        private static readonly HttpClient httpClient = new();
        private readonly IDatabase db;

        public PixivController(IConnectionMultiplexer connection) => db = connection.GetDatabase();

        [Route("[action]")]
        public async Task<string> IndexAsync()
        {
            var pixivHomeCache = db.StringGet("pixiv_cache");
            if (!pixivHomeCache.IsNullOrEmpty)
                return pixivHomeCache;
            var pixivHomePage = await httpClient.GetStringAsync("https://www.pixiv.net/");
            await db.StringSetAsync("pixiv_cache", pixivHomePage, TimeSpan.FromMinutes(5));
            return pixivHomePage;
        }
    }
}
