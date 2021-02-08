using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace lyc.xuming.studio.api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChromiumUrlsController : ControllerBase
    {
        static Dictionary<string, KeyValuePair<string, DateTime>> ChromiumBuilds = new();
        static string templatePrefix = "https://www.googleapis.com/download/storage/v1/b/chromium-browser-snapshots/o/";
        static string buildNumTemplate = templatePrefix + "{0}%2FLAST_CHANGE?alt=media";
        static Dictionary<string, string> downloadUrlTemplates = new();
        public ChromiumUrlsController()
        {
            if (ChromiumBuilds.Count == 0)
                foreach (var platformStr in new string[] { "Win", "Win_x64", "Linux", "Linux_x64", "Mac" })
                    ChromiumBuilds[platformStr] = new("", new DateTime(0));
            if (downloadUrlTemplates.Count == 0)
            {
                downloadUrlTemplates.Add("Win", templatePrefix + "Win%2F{0}%2Fchrome-win32.zip?alt=media");
                downloadUrlTemplates.Add("Win_x64", templatePrefix + "Win_x64%2F{0}%2Fchrome-win32.zip?alt=media");
                downloadUrlTemplates.Add("Linux", templatePrefix + "Linux%2F{0}%2Fchrome-linux.zip?alt=media");
                downloadUrlTemplates.Add("Linux_x64", templatePrefix + "Linux_x64%2F{0}%2Fchrome-linux.zip?alt=media");
                downloadUrlTemplates.Add("Mac", templatePrefix + "Mac%2F{0}%2Fchrome-mac.zip?alt=media");
            }
        }
        // GET: api/ChromiumUrls
        [HttpGet]
        public object Get()
        {
            var wc = new WebClient();
            var now = DateTime.Now;
            foreach (var platform in ChromiumBuilds.Keys.ToArray())
                if (now - ChromiumBuilds[platform].Value > TimeSpan.FromMinutes(30))
                    ChromiumBuilds[platform] = new(wc.DownloadString(String.Format(buildNumTemplate, platform)), now);
            return ChromiumBuilds.Keys.ToDictionary(platform => platform, platform => String.Format(downloadUrlTemplates[platform], ChromiumBuilds[platform].Key));
        }

        // GET: api/ChromiumUrls/{platform}
        [HttpGet("{platform}", Name = "Get")]
        public string Get(string platform)
        {
            if (!ChromiumBuilds.ContainsKey(platform)) return "";
            else
            {
                var wc = new WebClient();
                var now = DateTime.Now;
                if (now - ChromiumBuilds[platform].Value > TimeSpan.FromMinutes(30))
                    ChromiumBuilds[platform] = new(wc.DownloadString(String.Format(buildNumTemplate, platform)), now);
                return String.Format(downloadUrlTemplates[platform], ChromiumBuilds[platform].Key);
            }
        }
    }
}
