using LearnDapper.Model;
using LearnDapper.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            this._repo = repo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var _list = await this._repo.GetAll();
            if(_list==null)
            {
                return NotFound();
            }
            return Ok(_list);
        }

        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(int code)
        {
            var _list = await this._repo.Getbycode(code);
            if (_list == null)
            {
                return NotFound();
            }
            return Ok(_list);
        }

        [HttpGet("GetAllbyrole/{designation}")]
        public async Task<IActionResult> GetAllbyrole(string designation)
        {
            var _list = await this._repo.GetAllbyrole(designation);
            if (_list == null)
            {
                return NotFound();
            }
            return Ok(_list);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            var _list = await this._repo.Create(employee);            
            return Ok(_list);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Employee employee, int code)
        {
            var _list = await this._repo.Update(employee,code);
            return Ok(_list);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> RemoveByCode(int code)
        {
            var _list = await this._repo.Remove(code);
            if (_list == null)
            {
                return NotFound();
            }
            return Ok(_list);
        }


    }
}
