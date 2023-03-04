using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using System.Net;
using System.Linq;
using System.Text;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicLinkController : ControllerBase
    {
        static readonly Func<string, string> UrlDecode = WebUtility.UrlDecode;
        readonly HttpClient httpClient;
        public MusicLinkController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpHead("{id:int}/{rule?}")]
        [HttpGet("{id:int}/{rule?}")]
        public async Task<IActionResult> XiamiAsync(int id, string rule)
        {
            string ret = null;
            ret = JsonSerializer.Deserialize<dynamic>(await httpClient.GetStringAsync("http://www.xiami.com/widget/json-single/sid/" + id.ToString()))["location"].Value;
            int len = ret.Length - 1, line = int.Parse(ret[0].ToString()), lineLength = (len + line - 1) / line, shortLine = lineLength * line - len;
            var sb = new StringBuilder();
            ret = ret[1..];
            if (shortLine > 0)
            {
                for (int i = 0, blk = lineLength * (line - shortLine); i < blk; i += lineLength) sb.Append(ret.AsSpan(i, lineLength));
                for (int i = lineLength * (line - shortLine); i < len; i += lineLength - 1) sb.Append(ret.AsSpan(i, lineLength - 1)).Append(' ');
                ret = sb.ToString();
                sb.Clear();
            }
            for (int i = 0; i < len; i++)
                sb.Append(ret[i % line * lineLength + i / line]);
            string result = WebUtility.UrlDecode(sb.ToString()).Replace('^', '0');
            string target = string.IsNullOrEmpty(rule) ? result :
                JsonSerializer.Deserialize<string[][]>(rule).AsParallel().Aggregate(result, (ori, rep) => ori.Replace(UrlDecode(rep[0]), UrlDecode(rep[1])));
            return Redirect(target);
        }
    }
}