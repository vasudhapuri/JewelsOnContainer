using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")] //api/Pic
    [ApiController]
    public class PicController : ControllerBase
    {
        private readonly IWebHostEnvironment _env; 
        //environment is the location where microservice is hosted
        public PicController(IWebHostEnvironment env)
        {
            _env = env;
        }
        [Route ("{id}")]
        public IActionResult GetImage(int id)
        {
            var webroot = _env.WebRootPath; //WebRootPath gives location of wwwroot folder
            var path = Path.Combine($"{webroot}/pics/", $"Ring{id}.jpg"); //combine(path, filename)
            var buffer = System.IO.File.ReadAllBytes(path); //read image bytes from path
            return File(buffer, "image/jpeg");  //send content back to client, since client cant find pic folder thats on hard drive of server


        }
    }
}
