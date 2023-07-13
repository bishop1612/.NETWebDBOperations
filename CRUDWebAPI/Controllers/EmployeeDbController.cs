using System.Threading.Tasks;
using CRUDWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWebAPI.Controllers
{
    [Route("[controller]")]
    public class EmployeeDbController : ControllerBase
    {
        public sqlConnect Db { get; }

        public EmployeeDbController(sqlConnect db)
        {
            Db = db;
        }

        // GET /allrecords
        [HttpGet]
        [Route("allrecords")]
        public async Task<IActionResult> GetLatest()
        {
            await Db.Connection.OpenAsync();
            var query = new EmployeeDetailFilter(Db);
            var result = await query.GetAllAsync();
            return new OkObjectResult(result);
        }

        // GET find/5
        [HttpGet()]
        [Route("find/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new EmployeeDetailFilter(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            return new OkObjectResult(result);
        }

        // POST insert
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> Post([FromBody] EmployeeDetail body)
        {
            await Db.Connection.OpenAsync();
            body.Db = Db;
            await body.InsertAsync();
            return new OkObjectResult(body);
        }

        // PUT update/5
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> PutOne(int id, [FromBody] EmployeeDetail body)
        {
            await Db.Connection.OpenAsync();
            var query = new EmployeeDetailFilter(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            if (!String.IsNullOrEmpty(body.Name))
                result.Name = body.Name;
            if (body.Cost_Rate != 0)
                result.Cost_Rate = body.Cost_Rate;
            if (body.Bill_Rate != 0)
                result.Bill_Rate = body.Bill_Rate;
            await result.UpdateAsync();
            return new OkObjectResult(result);
        }

        // DELETE delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteOne(int id)
        {
            await Db.Connection.OpenAsync();
            var query = new EmployeeDetailFilter(Db);
            var result = await query.FindOneAsync(id);
            if (result is null)
                return new NotFoundResult();
            await result.DeleteAsync();
            return new OkResult();
        }
    }
}
