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
    public partial class MikanController : ControllerBase
    {
        private static readonly HttpClient httpClient = new();

        [Route("rss/{token}")]
        public async Task<ActionResult<string>> RSSAsync(string token)
        {
            var res = await httpClient.GetStringAsync($"https://mikanani.me/RSS/MyBangumi?token={token}");
            return Content(LinkMatcher().Replace(res, "magnet:?xt=urn:btih:$2"), "application/xml");
        }

        [GeneratedRegex("https://mikanani.me/Download/(.*?)/(.*?).torrent")]
        private static partial Regex LinkMatcher();
    }
}
