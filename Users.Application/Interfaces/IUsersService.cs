using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Core;

namespace Users.Application.Interfaces
{
    public interface IUsersService
    {
        User Create(User user);

        User GetById(long userId);

        User GetByEmail(string email);

        IEnumerable<User> GetAll();

        User Update(User user);

        bool Delete(long userId);
    }
}