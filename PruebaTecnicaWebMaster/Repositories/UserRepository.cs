using Microsoft.EntityFrameworkCore;
using PruebaTecnicaWebMaster.Models;

namespace PruebaTecnicaWebMaster.Repositories
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly BD_ControlVentasContext _dbContext;

        public UserRepository(BD_ControlVentasContext context)
        {
            _dbContext = context;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(int id)
        {
           return _dbContext.Users.Find(id);
        }

        public void Update(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }


    }


}
