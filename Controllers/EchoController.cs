using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lyc.xuming.studio.api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        [Route("[action]/{type_a?}/{type_b?}/{s}")]
        public ContentResult base64(string type_a, string type_b, string s) =>
            Content(s.Base64DecodeToString(), $"{type_a ?? "text"}/{type_b ?? "plain"}");
    }
}