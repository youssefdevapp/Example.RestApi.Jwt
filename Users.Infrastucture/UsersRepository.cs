using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.Core;
using Users.Infrastucture.Interfaces;

namespace Users.Infrastucture
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository()
        {
            using (var context = new UsersDbContext())
            {
                if (!context.Users.Any(u => u.Id == 1 || u.Id == 2))
                {
                    var authors = new List<User>
                    {
                        new User
                        {
                            Id = 1,
                            FullName = "Juan Pérez",
                            Age = 30,
                            Email = "juan@example.com",
                            Password = "secreto123",
                            RefreshToken = "abc123",
                            RefreshTokenEndDate = DateTime.Now.AddDays(7),
                            Role = "Usuario"
                        },
                        new User
                        {
                            Id = 2,
                            FullName = "María Rodríguez",
                            Age = 25,
                            Email = "maria@example.com",
                            Password = "contraseña456",
                            RefreshToken = "xyz789",
                            RefreshTokenEndDate = DateTime.Now.AddDays(7),
                            Role = "Administrador"
                        }
                    };

                    context.Users.AddRange(authors);
                    context.SaveChanges();
                }
            }
        }

        public List<User> Get()
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    return context.Users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    return context.Users.FirstOrDefault(item => item.Email.Equals(email));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public User GetById(long id)
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    return context.Users.FirstOrDefault(item => item.Id.Equals(id));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public void Add(User user)
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public void Remove(User user)
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(User user)
        {
            try
            {
                using (var context = new UsersDbContext())
                {
                    context.Entry(user).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }
    }
}