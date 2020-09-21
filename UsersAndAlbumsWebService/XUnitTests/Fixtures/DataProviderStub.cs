using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAndAlbumsWebService.Data;
using UsersAndAlbumsWebService.Models;

namespace UsersAndAlbumsWebServiceTest.Fixtures
{
    internal class DataProviderStub : IDataProvider
    {
        public Task<Album> GetAlbum(int id)
        {
            return Task.Run(() => albums.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<Album>> GetAlbumByUserId(int userId)
        {
            return Task.Run(() => albums.Where(a => a.UserId == userId));
        }

        public Task<IEnumerable<Album>> GetAlbums()
        {
            return Task.Run(() => albums.AsEnumerable());
        }

        public Task<User> GetUser(int id)
        {
            return Task.Run(() => users.FirstOrDefault(a => a.Id == id));
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            return Task.Run(() => users.AsEnumerable());
        }

        private readonly List<User> users = new List<User>() {
            new User() { Id = 1 },
            new User() { Id = 2 }
        };

        private readonly List<Album> albums = new List<Album>() {
            new Album() { Id = 1, UserId = 1, Title = "Simple title" },
            new Album() { Id = 2, UserId = 1, Title = "Best ever title"}
        };
    }
}
