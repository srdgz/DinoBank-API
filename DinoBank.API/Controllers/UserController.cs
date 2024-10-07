using DinoBank.Domain.User;
using DinoBank.Persistence.Database;
using Microsoft.AspNetCore.Mvc;

namespace DinoBank.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        public UserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var data = _databaseService.GetAll();
            if (data != null && data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, data);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var data = _databaseService.GetAll();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);
            var user = data.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet("get-by-userName/{userName}")]
        public IActionResult GetByUsername(string userName)
        {
            var data = _databaseService.GetAll();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);
            var user = data.FirstOrDefault(x => x.UserName == userName);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpGet("get-by-type/{type}")]
        public IActionResult GetByType(string type)
        {
            var data = _databaseService.GetAll();
            if (data == null || data.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);
            var users = data.Where(x => x.Type == type).ToList();
            if (users == null || users.Count == 0)
                return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] UserEntity user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type))
                {
                return StatusCode(StatusCodes.Status400BadRequest, "Parámetros no válidos");
                }
            var data = _databaseService.Create(user);
            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UserEntity user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Type) || user.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Parámetros no válidos");
            }
            var data = _databaseService.Update(user);
            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Id no válido");
            var data = _databaseService.Delete(id);
            if (!data)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
