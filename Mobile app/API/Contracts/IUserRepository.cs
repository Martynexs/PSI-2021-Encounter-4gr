using EncounterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<UserModel>
    {
        void CreateUser(UserModel user);
        void DeleteUser(UserModel user);
        void UpdateUser(UserModel user);

        Task<IEnumerable<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(long Id);
        Task<UserModel> GetUserByUsername(string username);
    }
}
