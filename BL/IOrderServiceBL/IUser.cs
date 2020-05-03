using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.IOrderServiceBL
{
   public interface IUser
    {
        object GetAllUsers();
        Task<object> UpdateAllUsers(Users data);
        Task<object> AddAllUsers(Users data);
        Task<object> DeleteUserData(int data);

    }
}
