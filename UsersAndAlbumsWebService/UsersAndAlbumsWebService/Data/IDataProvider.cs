using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAndAlbumsWebService.Models;

namespace UsersAndAlbumsWebService.Data
{
    public interface IDataProvider
    {

        /// <summary>
        /// Получает все альбомы пользователя с указанным Id
        /// </summary>
        /// <param name="userId">Id пользователя </param>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetAlbumByUserId(int userId);

        /// <summary>
        /// Получает все альбомы
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Album>> GetAlbums();

        /// <summary>
        /// Получает данные всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<User>> GetUsers();

        /// <summary>
        /// Получает данные пользователя с указанным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetUser(int id);

        /// <summary>
        /// Получает данные альбома с указанным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Album> GetAlbum(int id);
    }
}
