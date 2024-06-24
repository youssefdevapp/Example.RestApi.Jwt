using System.Collections.Generic;
using Users.Application.Interfaces;
using Users.Core;
using Users.Infrastucture.Interfaces;

namespace Users.Application
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User Create(User user)
        {
            _usersRepository.Add(user);

            return user;
        }

        public bool Delete(long userId)
        {
            var user = _usersRepository.GetById(userId);

            _usersRepository.Remove(user);

            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _usersRepository.Get();
        }

        public User GetById(long userId)
        {
            return _usersRepository.GetById(userId);
        }

        public User GetByEmail(string email)
        {
            return _usersRepository.GetByEmail(email);
        }

        public User Update(User user)
        {
            _usersRepository.Update(user);

            return user;
        }
    }
}