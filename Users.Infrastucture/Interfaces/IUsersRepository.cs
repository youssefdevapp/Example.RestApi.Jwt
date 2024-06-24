using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Core;

namespace Users.Infrastucture.Interfaces
{
    public interface IUsersRepository
    {
        List<User> Get();

        User GetById(long id);

        User GetByEmail(string email);

        void Add(User user);

        void Remove(User user);

        Task Update(User user);
    }
}