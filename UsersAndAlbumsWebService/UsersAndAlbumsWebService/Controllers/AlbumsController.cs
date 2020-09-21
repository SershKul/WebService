using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsersAndAlbumsWebService.Data;
using UsersAndAlbumsWebService.Models;

namespace UsersAndAlbumsWebService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;

        public AlbumsController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Возвращает все альбомы или альбомы указанного пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <returns>Список альбомов</returns>
        // GET: api/<AlbumsController>?userid=2
        [HttpGet]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(List<Album>), 200)] //Из-за ошибки в swagger временное решение использовать этот конструктор атрибута
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetAlbumsByUserId(int userId)
        {
            try
            {
                if (userId != 0)
                {
                    IEnumerable<Album> albums = await _dataProvider.GetAlbumByUserId(userId);
                    return Ok(albums);
                }
                else
                {
                    IEnumerable<Album> albums = await _dataProvider.GetAlbums();
                    return Ok(albums);
                }
            }
            catch
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Возвращает альбом по Id
        /// </summary>
        /// <param name="id">id альбома</param>
        /// <returns>Один альбом</returns>
        // GET: api/<AlbumsController>/2       
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml")]
        [ProducesResponseType(typeof(Album), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            try
            {
                Album album = await _dataProvider.GetAlbum(id);
                return Ok(album);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
