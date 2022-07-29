using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;
using MoeBinAPI.Data;
using MoeBinAPI.Utils;
using System.Security.Cryptography;
using MoeBinAPI.Models;

namespace MoeBinAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class Controller : ControllerBase
    {
        private readonly PasteRepo _repo;

        public Controller(PasteRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("new")]
        public async Task<ActionResult> NewPaste([FromForm]string content)
        {
            if(content == null || content == String.Empty)
            {
                return BadRequest("Content cannot be empty");
            }
            string ip = Request.Host.Host;
            var paste = new Paste();         
            string key;
            using(Aes aes = Aes.Create())
            {
                paste.Iv = aes.IV;
                paste.PasteId = _repo.GetPasteID();
                key = Convert.ToBase64String(aes.Key);
                System.Console.WriteLine(paste.PasteId);
                paste.CreatedBy = ip;
                await FileHandler.WriteFile(paste.PasteId, Security.Encrypt(content, aes.Key, aes.IV));
            }
            string uriKey = HttpUtility.UrlEncode(key);
            _repo.CreatePaste(paste);
            string createdAt = "https://localhost:7173/get?id=" + paste.PasteId 
                + "&key=" + uriKey;
            return _repo.Commit() ? Created(createdAt, new {location = createdAt, id = paste.PasteId, key = key}) : Problem("Something went wrong");
        }

        [HttpGet]
        [Route("get")]
        public ActionResult RetriveContent(string id, string key)
        {
            string b64 = HttpUtility.UrlDecode(key);
            System.Console.WriteLine(b64);
            byte[] keyB = Convert.FromBase64String(Request.Query["key"].ToString());
            var p = _repo.GetPaste(id);
            if(p == null)
            {
                return NotFound();
            }
            byte[]? encrypted = FileHandler.ReadFile(p.PasteId);
            if(encrypted == null)
            {
                return NoContent();
            }

            string plain = Security.Decrypt(encrypted, keyB, p.Iv);

            // return Ok(new { content = plain});
            return Ok(plain);
        }
    }
}