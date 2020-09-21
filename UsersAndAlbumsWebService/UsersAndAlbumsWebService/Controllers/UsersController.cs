using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UsersAndAlbumsWebService.Data;
using UsersAndAlbumsWebService.Models;

namespace UsersAndAlbumsWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDataProtector _protector;
        private readonly IDataProvider _dataProvider;

        public UsersController(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration, IDataProvider dataProvider)
        {
            _configuration = configuration;
            _protector = dataProtectionProvider.CreateProtector(_configuration["Key"]);
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Возвращает список даных всех пользователей и скрывает Email
        /// </summary>
        /// <returns>Список пользователей</returns>
        // GET: api/<UserController>
        [HttpGet]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(List<User>), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _dataProvider.GetUsers();
                foreach (var u in users)
                {
                    if (!string.IsNullOrEmpty(u.Email))
                        u.Email = _protector.Protect(u.Email);
                }

                return Ok(users);
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Возвращает данные указанного пользователя и скрывает Email
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Данные одного пользователя</returns>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                User user = await _dataProvider.GetUser(id);
                if (!string.IsNullOrEmpty(user.Email))
                    user.Email = _protector.Protect(user.Email);
                return Ok(user);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
