using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserRopository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRopository(EncounterContext encounterContext)
            : base(encounterContext)
        {
        }

        public void CreateUser(UserModel user)
        {
            Create(user);
        }

        public void DeleteUser(UserModel user)
        {
            Delete(user);
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<UserModel> GetUserById(long Id)
        {
            return await FindByCondition(x => x.ID == Id).FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByUsername(string username)
        {
            return await FindByCondition(x => x.Username == username).FirstOrDefaultAsync();
        }

        public void UpdateUser(UserModel user)
        {
            Update(user);
        }
    }
}
