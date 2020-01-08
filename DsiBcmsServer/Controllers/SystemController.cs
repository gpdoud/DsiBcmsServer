using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DSI.BcmsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSI.BcmsServer.Controllers {

    [Route("dsi/sys")]
    [ApiController]
    public class SystemController : ControllerBase {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private DsiBcmsContext _context = null;
        public SystemController(DsiBcmsContext context) { _context = context; }
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        [HttpGet("{key}")]
        public async Task<ActionResult<SystemControl>> GetKey(string key) {
            logger.Trace($"key = {key}");
            var sysctrl = await _context.SysCtrls.FindAsync(key);
            if(sysctrl == null) return new NotFoundResult();
            return new OkObjectResult(sysctrl);
        }

        [HttpPost("{key}/{value}")]
        public async Task<IActionResult> SetKey(string key, string value, string category = null) {
            logger.Trace($"key = {key}, value = {value}, category = {category}");
            try {
                // if key exists, update it
                var sysctrl = await _context.SysCtrls.FindAsync(key);
                if(sysctrl == null) { // doesn't exist; add it
                    var sc = new SystemControl {
                        Key = key,
                        Value = value,
                        Category = category,
                        Active = true
                    };
                    _context.SysCtrls.Add(sc);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(sc);
                }
                // else add the key/value
                sysctrl.Value = value;
                sysctrl.Updated = DateTime.Now;
                await _context.SaveChangesAsync();
                return new OkObjectResult(sysctrl);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpGet("/")]
        public IActionResult GetStatus() {
            try {
                var msg = new {
                    Name = $"Boot Camp Management System (BCMS)",
                    Version = "0.0",
                    Author = "Doud Systems",
                    State = "ALPHA",
                    DateTime = $"{DateTime.Now}",
                    Status = 200,
                    Result = "Ok"
                };
                logger.Trace(msg);
                return new JsonResult(msg, jsonOptions);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }
    }
}