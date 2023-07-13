using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDWebAPI.Models;
using CRUDWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDWebAPI.Controllers
{
    [Route("[controller]")]
    public class LaborHoursDbController : Controller
    {
        private readonly MongoDBService _mongoDBService;

        public LaborHoursDbController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        [Route("allrecords")]
        public async Task<List<EmployeeLaborHours>> Get()
        {
            return await _mongoDBService.GetAsync();
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Post([FromBody] EmployeeLaborHours laborhour)
        {
            await _mongoDBService.CreateAsync(laborhour);
            return CreatedAtAction(nameof(Get), new { id = laborhour.Id }, laborhour);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] EmployeeLaborHours body)
        {
            await _mongoDBService.UpdateEntry(id, body.Bill_Hours, body.Non_Bill_Hours, body.Week_Ending_Date);
            return NoContent();
        }

        // DELETE delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mongoDBService.DeleteAsync(id);
            return NoContent();
        }

    }
}

