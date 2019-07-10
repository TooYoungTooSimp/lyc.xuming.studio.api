using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpHead("{id:int}/{rule?}")]
        [HttpGet("{id:int}/{rule?}")]
        public IActionResult Xiami(int id, string rule)
        {
            string ret = null;
            using (var wc = new WebClient())
                ret = JsonConvert.DeserializeObject<dynamic>(wc.DownloadString("http://www.xiami.com/widget/json-single/sid/" + id.ToString()))["location"].Value;
            int len = ret.Length - 1, line = Int32.Parse(ret[0].ToString()), lineLength = (len + line - 1) / line, shortLine = lineLength * line - len;
            var sb = new StringBuilder();
            ret = ret.Substring(1);
            if (shortLine > 0)
            {
                for (int i = 0, blk = lineLength * (line - shortLine); i < blk; i += lineLength) sb.Append(ret.Substring(i, lineLength));
                for (int i = lineLength * (line - shortLine); i < len; i += lineLength - 1) sb.Append(ret.Substring(i, lineLength - 1)).Append(' ');
                ret = sb.ToString();
                sb.Clear();
            }
            for (int i = 0; i < len; i++)
                sb.Append(ret[i % line * lineLength + i / line]);
            string result = WebUtility.UrlDecode(sb.ToString()).Replace('^', '0');
            string target = String.IsNullOrEmpty(rule) ? result :
                JsonConvert.DeserializeObject<string[][]>(rule).AsParallel().Aggregate(result, (ori, rep) => ori.Replace(UrlDecode(rep[0]), UrlDecode(rep[1])));
            return Redirect(target);
        }
    }
}