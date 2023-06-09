using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using lyc.xuming.studio.api.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IDatabase db;
        private readonly IConfiguration config;
        private static bool SpanEqual(ReadOnlySpan<byte> span1, ReadOnlySpan<byte> span2) => span1.SequenceEqual(span2);
        public StatusController(IConnectionMultiplexer connection, IConfiguration configuration)
        {
            config = configuration;
            db = connection.GetDatabase();
        }

        // GET: api/Status/5
        [HttpGet("{key}")]
        public async Task<ActionResult<string>> GetAsync(string key)
        {
            if (!db.KeyExists(key)) return NotFound();
            return (string)await db.StringGetAsync(key);
        }

        // POST: api/Status
        [HttpPost("{key}")]
        public async Task<ActionResult> PostAsync(string key)
        {
            var (valueRaw, secretRaw) = (Request.Form["value"], Request.Form["secret"]);
            if (valueRaw.Count != 1 || secretRaw.Count != 1) return BadRequest();
            var (value, secret) = (valueRaw.First(), secretRaw.First());
            var (secret_hash, target_hash) = (SHA512.HashData(Encoding.Default.GetBytes(secret)), HexToBytes.Convert(config["Secrets:StatusUpdateKeyHash"]));
            if (!SpanEqual(secret_hash, target_hash))
                return Unauthorized();
            if (await db.StringSetAsync(key, value))
                return Ok();
            else
                return StatusCode(500);
        }
    }
}
