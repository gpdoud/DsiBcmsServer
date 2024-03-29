﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DSI.BcmsServer.Models;
using DSI.BcmsServer.ModelViews;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DSI.BcmsServer.Controllers {

	[Route("dsi/[controller]")]
	[ApiController]
	public class LogsController : ControllerBase {

		private readonly DsiBcmsContext _context;

		public LogsController(DsiBcmsContext context) { _context = context; }

		[HttpDelete("purge")]
		public async Task<IActionResult> PurgeLogsBefore() {
			var keyLogsMonthsRetention = "logs.months.retention";
			var defaultMonthsRetention = 6;
			var cfgCtrl = new ConfigsController(_context);
			var rc = await cfgCtrl.GetKey(keyLogsMonthsRetention);
			var configLogsMonthsRetention = rc.Value;
			if (configLogsMonthsRetention == null) {
				configLogsMonthsRetention = new Config() {
					KeyValue = keyLogsMonthsRetention, DataValue = defaultMonthsRetention.ToString()
				};
				await cfgCtrl.AddUpdateKey(configLogsMonthsRetention);
			}
			var logsMonthsToRetain = Convert.ToInt32(configLogsMonthsRetention.DataValue);
			return await PurgeLogsBefore(logsMonthsToRetain);
		}

		[HttpDelete("purge/{monthsToRetain}")]
		public async Task<IActionResult> PurgeLogsBefore(int monthsToRetain) {
			if (monthsToRetain < 1) { return BadRequest(); }
			var now = DateTime.Now.AddMonths(-monthsToRetain);
			var purgeBeforeDate = new DateTime(now.Year, now.Month, 1);
			var logs = await _context.Logs.Where(l => l.Timestamp < purgeBeforeDate).ToListAsync();
			_context.Logs.RemoveRange(logs);
			await _context.SaveChangesAsync();
			return NoContent();
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Log>>> GetAll() {
			return await _context.Logs.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Log>> Get(int id) {
			var log = await _context.Logs.FindAsync(id);
			if (log == null) return NotFound();
			return log;
		}

		private async Task<IActionResult> Log(Log log) {
			log.Timestamp = Utility.Date.EasternTimeNow;
			_context.Add(log);
			await _context.SaveChangesAsync();

			return CreatedAtAction("Get", new { id = log.Id }, log);
		}

		[HttpPost]
		public async Task<IActionResult> LogCreate(Log log) {
			return await Log(log);
		}

		[HttpPost]
		private async Task<IActionResult> LogMessage(string message, LogSeverity sev) {
			var log = new Log {
				Id = 0,
				Message = message,
				Severity = sev,
				Timestamp = Utility.Date.EasternTimeNow
			};
			return await Log(log);
		}

		[HttpPost("info")]
		public async Task<IActionResult> LogInfo(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Info);
		}

		[HttpPost("warn")]
		public async Task<IActionResult> LogWarning(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Warn);
		}

		[HttpPost("error")]
		public async Task<IActionResult> LogError(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Error);
		}

		[HttpPost("fatal")]
		public async Task<IActionResult> LogFatal(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Fatal);
		}

		[HttpPost("trace")]
		public async Task<IActionResult> LogTrace(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Trace);
		}

		[HttpPost("debug")]
		public async Task<IActionResult> LogDebug(LogMessage lm) {
			return await LogMessage(lm.Message, LogSeverity.Debug);
		}

		// POST: api/Logs/Update/5
		[HttpPost("update/{id}")]
		public async Task<IActionResult> UpdateKb(int id, Log log) {
			return await PutKb(id, log);
		}

		// PUT: api/Logs/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutKb(int id, Log log) {
			if (id != log.Id) {
				return BadRequest();
			}

			_context.Entry(log).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!LogExists(id)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return NoContent();
		}

		// DELETE: api/Kbs/5
		[HttpPost("delete/{id}")]
		public async Task<ActionResult<Log>> RemoveLog(int id) {
			return await DeleteLog(id);
		}

		// DELETE: api/Kbs/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Log>> DeleteLog(int id) {
			var log = await _context.Logs.FindAsync(id);
			if (log == null) {
				return NotFound();
			}

			_context.Logs.Remove(log);
			await _context.SaveChangesAsync();

			return log;
		}

		private bool LogExists(int id) {
			return _context.Logs.Any(e => e.Id == id);
		}
	}
}