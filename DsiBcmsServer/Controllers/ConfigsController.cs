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

    [Route("dsi/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private DsiBcmsContext _context = null;
        public ConfigsController(DsiBcmsContext context) { _context = context; }
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Config>>> GetAll() {
            return await _context.Configs.ToListAsync();
        }

        [HttpGet("{key}")]
        public async Task<ActionResult<Config>> GetKey(string key) {
            logger.Trace($"key = {key}");
            var sysctrl = await _context.Configs.FindAsync(key);
            if(sysctrl == null) return NotFound();
            return sysctrl;
        }

        [HttpGet("search/{partKey}")]
        public async Task<ActionResult<IEnumerable<Config>>> GetPartialKey(string partKey) {
            return await _context.Configs.Where(x => x.KeyValue.StartsWith(partKey)).ToListAsync();
        }

        [HttpGet("keys/{keyList}")]
        public async Task<ActionResult<IEnumerable<Config>>> GetKeys(string keyList) {
            var keys = keyList.Split(','); // split keys with comma separator
            var configs = new List<Config>();
            foreach(var key in keys) {
                var config = await GetKey(key);
                if(config.Value == null) continue;
                configs.Add(config.Value);
            }
            return configs;
        }

        [HttpPost()]
        public async Task<IActionResult> AddUpdateKey(Config sysctrl) {
            logger.Trace($"sysctrl = {sysctrl}");
            try {
                // if key exists, update it
                var sc = await _context.Configs.FindAsync(sysctrl.KeyValue);
                if(sc == null) { // doesn't exist; add it
                    logger.Trace($"Adding new key {sysctrl.KeyValue}");
                    sysctrl.Created = DateTime.Now.ToUniversalTime();
                    _context.Configs.Add(sysctrl);
                    await _context.SaveChangesAsync();
                    return new OkObjectResult(sc);
                }
                // else add the key/value
                logger.Trace($"Updating key {sysctrl.KeyValue}");
                sc.DataValue = sysctrl.DataValue;
                sc.System = sysctrl.System;
                sc.Active = sysctrl.Active;
                sc.Updated = DateTime.Now.ToUniversalTime();
                await _context.SaveChangesAsync();
                return new OkObjectResult(sc);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpDelete("{key}")]
        public async Task<ActionResult<Config>> Remove(string key) {
            try {
                var cfg = await _context.Configs.FindAsync(key);
                if(cfg == null) return NotFound();
                _context.Configs.Remove(cfg);
                await _context.SaveChangesAsync();
                return cfg;
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetStatus() {
            try {
                var Configs = await _context.Configs.Where(x => x.System).ToArrayAsync();
                return new JsonResult(Configs, jsonOptions);
            } catch(Exception ex) {
                return new JsonResult(ex);
            }
        }
    }
}