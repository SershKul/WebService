using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAndAlbumsWebService.Data;
using UsersAndAlbumsWebService.Models;
using System.Net.Http;

namespace UsersAndAlbumsWebServiceTest.Fixtures
{
    internal class InvalidDataProviderStub : IDataProvider
    {
        public Task<IEnumerable<Album>> GetAlbumByUserId(int userId)
        {
            throw new HttpRequestException();
        }

        public Task<IEnumerable<Album>> GetAlbums()
        {
            throw new HttpRequestException();
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new HttpRequestException();
        }

        public Task<User> GetUser(int id)
        {
            throw new HttpRequestException();
        }

        public Task<Album> GetAlbum(int id)
        {
            throw new HttpRequestException();
        }
    }
}
