using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MikanController : ControllerBase
    {
        private static readonly HttpClient httpClient = new();
        private static readonly Regex linkMatcher = new("https://mikanani.me/Download/(.*?)/(.*?).torrent");

        [Route("rss/{token}")]
        public async Task<ActionResult<string>> RSSAsync(string token)
        {
            var res = await httpClient.GetStringAsync($"https://mikanani.me/RSS/MyBangumi?token={token}");
            return Content(linkMatcher.Replace(res, "magnet:?xt=urn:btih:$2"), "application/xml");
        }
    }
}
