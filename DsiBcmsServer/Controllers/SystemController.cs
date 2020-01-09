using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DSI.BcmsServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost()]
        public async Task<IActionResult> AddUpdateKey(SystemControl sysctrl) {
            logger.Trace($"sysctrl = {sysctrl}");
            try {
                // if key exists, update it
                var sc = await _context.SysCtrls.FindAsync(sysctrl.Key);
                if(sc == null) { // doesn't exist; add it
                    logger.Trace($"Adding new key {sysctrl.Key}");
                    sysctrl.Created = DateTime.Now.ToUniversalTime();
                    _context.SysCtrls.Add(sysctrl);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(sc);
                }
                // else add the key/value
                logger.Trace($"Updating key {sysctrl.Key}");
                sc.Value = sysctrl.Value;
                sc.Category = sysctrl.Category;
                sc.Active = sysctrl.Active;
                sc.Updated = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
                return new OkObjectResult(sc);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetStatus() {
            try {
                var sysctrls = await _context.SysCtrls.Where(x => x.Category.Equals("system")).ToArrayAsync();
                return new JsonResult(sysctrls, jsonOptions);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }
    }
}