using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSI.BcmsServer.Controllers {

    [Route("dsi/[controller]")]
    [ApiController]
    public class LogController : ControllerBase {

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [HttpPost("/debug")]
        public void Debug(Utility.Log log) {
            logger.Debug("{0}", log.Message);
        }

        [HttpPost("/trace")]
        public void Trace(Utility.Log log) {
            logger.Trace("{0}", log.Message);
        }

        [HttpPost]
        public void Log(Utility.Log log) {
            Info(log);
        }

        [HttpPost("/info")]
        public void Info(Utility.Log log) {
            logger.Info("{0}", log.Message);
        }

        [HttpPost("/warn")]
        public void Warning(Utility.Log log) {
            logger.Warn("{0}", log.Message);
        }

        [HttpPost("/error")]
        public void Error(Utility.Log log) {
            logger.Error("{0}", log.Message);
        }

    }
}