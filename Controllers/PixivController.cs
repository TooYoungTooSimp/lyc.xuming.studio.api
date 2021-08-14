using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private static readonly HttpClient HttpClient = new();
        [Route("[action]")]
        public async Task<string> IndexAsync()
        {
            return await HttpClient.GetStringAsync("https://www.pixiv.net/");
        }
    }
}
