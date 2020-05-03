using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.IOrderServiceBL
{
   public interface IOrderService
    {
        Task<object> GetOrderDetails(string jsondata);
        Task<object> AddorderDetails(string jsondata);
        //Task<object> GetAdminOrderDetails();
        Task<object> UpdateOrderDetails(Order jsondata);
        Task<object> UpdateOrderStatus(Order jsondata);
        Task<object> DeleteOrder(Order jsondata);
    }
}
