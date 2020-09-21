using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UsersAndAlbumsWebService.Models;

namespace UsersAndAlbumsWebService.Data
{
    
    public class DataProvider : IDataProvider
    {
        private const string url = "http://jsonplaceholder.typicode.com";


        /// <summary>
        /// Получает все альбомы пользователя с указанным Id
        /// </summary>
        /// <param name="userId">Id пользователя </param>
        /// <returns>Список альбомов</returns>
        public async Task<IEnumerable<Album>> GetAlbumByUserId(int userId)
        {
            string urlAddition = "/albums?userId=" + userId.ToString();
            return await GetData<IEnumerable<Album>>(urlAddition);
        }

        /// <summary>
        /// Получает все альбомы
        /// </summary>
        /// <returns>Список альбомов</returns>
        public async Task<IEnumerable<Album>> GetAlbums()
        {
            string urlAddition = "/albums";
            return await GetData<IEnumerable<Album>>(urlAddition);
        }

        /// <summary>
        /// Получает данные всех пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        public async Task<IEnumerable<User>> GetUsers()
        {
            string urlAddition = "/users";
            return await GetData<IEnumerable<User>>(urlAddition);
        }

        /// <summary>
        /// Получает данные пользователя с указанным Id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> GetUser(int id)
        {
            string urlAddition = "/users/" + id.ToString();
            return await GetData<User>(urlAddition);
        }

        /// <summary>
        /// Получает данные альбома с указанным Id
        /// </summary>
        /// <param name="id">id альбома</param>
        /// <returns>Альбом</returns>
        public async Task<Album> GetAlbum(int id)
        {
            string urlAddition = "/albums/" + id.ToString();
            return await GetData<Album>(urlAddition);
        }

        private async Task<T> GetData<T>(string urlAddition)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url + urlAddition);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(responseBody);
                return result;
            }
        }
    }
}
