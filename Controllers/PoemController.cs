using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace lyc.xuming.studio.api.Controllers
{
    [Route("api/[controller]")]
    public class PoemController : Controller
    {
        [HttpGet]
        public string Get() => HaPoem.GetString();
        [HttpGet("{id}")]
        public string Get(int id) => HaPoem.GetString(id);
    }
    static class HaPoem
    {
        static private string[] strs = File.ReadAllLines("poem.txt");
        static private Random rnd = new Random();
        static public string GetString() => strs[rnd.Next(0, strs.Length)];
        static public string GetString(int id) => strs[id % strs.Length];
    }
}