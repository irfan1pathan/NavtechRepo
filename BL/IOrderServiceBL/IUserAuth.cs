using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.IOrderServiceBL
{
    public interface IUserAuth
    {
        object Authenticate(string username, string password);
    }
}
