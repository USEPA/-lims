﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.Storage.SQLite;
using Hangfire.Storage.Monitoring;

namespace LimsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        // GET: api/Tasks
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var monAPI = JobStorage.Current.GetMonitoringApi();
            var stats = monAPI.GetStatistics();
            
           
            return new string[] { "value1", "value2" };
        }

        // GET: api/Tasks/5
        [HttpGet("{name}")]
        public string Get(int name)
        {
            return "value";
        }

        // POST: api/Tasks
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
