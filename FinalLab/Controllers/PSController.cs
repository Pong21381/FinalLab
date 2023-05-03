using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalLab.Database;
using FinalLab.Models;

namespace FinalLab.Controllers
{
    //https://localhost:7203/api/manufacturer
    [Route("api/[controller]")]
    [ApiController]
    public class positionController : ControllerBase
    {
        //Variable
        private readonly DataDbContext _dbContext;

        //Cotructure Method
        public positionController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        //get push put delete
        //get
        [HttpGet]
        public async Task<ActionResult<List<positions>>> GetPositions()
        {
            var positions = await _dbContext.positions.ToListAsync();

            if (positions.Count == 0)
            {
                return NotFound();
            }

            return Ok(positions);
        }

        //get by id
        [HttpGet("id")]
        public async Task<ActionResult<positions>> GetPositionsID(string id)
        {
            var positions = await _dbContext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }

        //get by getPositionsID
        [HttpGet("PositionsID")]
        public async Task<ActionResult<positions>> GetEmpPositionsID(string id)
        {
            var positions = _dbContext.employees.FirstOrDefault(e => e.empId == id);
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }


        //Post
        [HttpPost]
        public async Task<ActionResult<positions>> PostPosition(positions positions)
        {
            try
            {
                _dbContext.positions.Add(positions);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(positions);
        }

        //Put
        [HttpPut]
        public async Task<ActionResult<positions>> PutPosition(int id, positions newPositions)
        {
            var positions = await _dbContext.positions.FindAsync(id);
            if (positions == null)
            {
                return NotFound();
            }

            positions.positionId = newPositions.positionId;
            positions.positionName = newPositions.positionName;
            positions.baseSalary = newPositions.baseSalary;
            positions.salaryIncreaseRate = newPositions.salaryIncreaseRate;

            await _dbContext.SaveChangesAsync();
            return Ok(positions);
        }

        //Delete
        [HttpDelete]
        public async Task<ActionResult<positions>> DeletePositions(string id)
        {
            var employees = _dbContext.employees.Where(e => e.positionId == id).ToList();
            if (employees != null && employees.Count > 0)
            {
                return BadRequest("Cannot delete position with employees assigned to it.");
            }
            var position = _dbContext.positions.SingleOrDefault(p => p.positionId == id);
            if (position == null)
            {
                return NotFound();
            }
            _dbContext.positions.Remove(position);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
