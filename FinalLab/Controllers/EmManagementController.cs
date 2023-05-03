using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalLab.Database;
using FinalLab.Models;

namespace FinalLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        private readonly DataDbContext _dbContext;
        public employeeController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        //All
        [HttpGet]
        public async Task<ActionResult<List<employees>>> GetEmployees()
        {
            var employees = await _dbContext.employees.ToListAsync();

            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // Get id
        [HttpGet("id")]
        public async Task<ActionResult<employees>> GetEmployeeById(int id)
        {
            var employees = await _dbContext.employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        //Current Salary 
        [HttpGet("current year")]
        public async Task<ActionResult<employees>> GetSalarycurrentYear(string id)
        {

            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var yearsOfWork = (DateTime.Now.Year - employee.HireDate.Year) - 1;

            var position = _dbContext.positions.Find(employee.positionId);
            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate)) * yearsOfWork;

            return Ok(salary);
        }

        //Guess Salary 
        [HttpGet("guess Salary")]
        public async Task<ActionResult<employees>> GetGuessSalary(string id, int year)
        {

            var employee = _dbContext.employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            var position = _dbContext.positions.Find(employee.positionId);
            if (position == null)
            {
                return NotFound();
            }
            var salary = (position.baseSalary + (position.baseSalary * position.salaryIncreaseRate)) * (year - 1);

            return Ok(salary);
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<employees>> CreateEmployees(employees employees)
        {

            try
            {
                var position = _dbContext.positions.FirstOrDefault(p => p.positionId == employees.positionId);
                if (position == null)
                {
                    return BadRequest("Invalid position ID");
                }

                _dbContext.employees.Add(employees);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok(employees);
        }

        //Put
        [HttpPut]
        public async Task<ActionResult<employees>> putEmployees(string id, employees newEmployees)
        {
            try
            {
                if (_dbContext.positions.FirstOrDefault(p => p.positionId == newEmployees.positionId) == null)
                {
                    return BadRequest("Invalid position ID");
                }

                var employees = await _dbContext.employees.FindAsync(id);
                if (employees == null)
                {
                    return NotFound();
                }


                employees.empName = newEmployees.empName;
                employees.Email = newEmployees.Email;
                employees.phoneNumber = newEmployees.phoneNumber;
                employees.HireDate = newEmployees.HireDate;
                employees.positionId = newEmployees.positionId;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }




            return Ok(newEmployees);
        }


        //Delete

        [HttpDelete]
        public async Task<ActionResult<employees>> deleteEmployees(string id)
        {
            var employees = await _dbContext.employees.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            //Remove
            _dbContext.employees.Remove(employees);

            //save
            await _dbContext.SaveChangesAsync();

            return Ok(employees);
        }



    }
}
