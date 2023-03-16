using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.IO;

namespace WebPageCaching.Controllers
{
    public class CacheController : Controller
    {
        private readonly IDistributedCache _distributedCache;

        public CacheController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Views/Test.cshtml");
            byte[] fileByte=System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("dosya", fileByte);
            return View();
        }
        public IActionResult ShowFile()
        {
            byte[] webPage = _distributedCache.Get("dosya");
            return File(webPage,"text/html");
        }
    }
}
