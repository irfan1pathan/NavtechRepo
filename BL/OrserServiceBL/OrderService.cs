using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderManagementSystem.BL.IOrderServiceBL;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.BL.OrserServiceBL
{
    public class OrderService : IOrderService
    {
        private navtechContext Context;
        public OrderService(navtechContext context)
        {
            this.Context = context;
        }

        public async Task<object> GetOrderDetails(string jsondata)
        {
            var sqlParameters = new SqlParameter(StoredProcedureParams.Jsondata,jsondata);
            var result = await Context.ResultModel.FromSql(Constants.sp_productDetails, sqlParameters).AsNoTracking().ToListAsync();
            return JObject.Parse(result[0].result);
        }

       
        public async Task<object> AddorderDetails(string jsondata)
        {
            ResponseObj obj = new ResponseObj();
            var sqlParameters = new SqlParameter(StoredProcedureParams.Jsondata, jsondata);
            var result = await Context.DBStatus.FromSql(Constants.sp_AddOrderDetails, sqlParameters).AsNoTracking().FirstOrDefaultAsync();
            if(result.DBResult==1)
            {
                obj.result = true;
                obj.Message = Messages.InsertSuccess;
            }
            else
            {
                obj.result = false;
                obj.Message = Messages.SomethingWentWrong;
            }
            return obj;
        }

        public async Task<object> UpdateOrderStatus(Order data)
        {
            ResponseObj obj = new ResponseObj();

            var result = await Context.Order.Where(x => x.UserId == data.UserId).ToListAsync();

            if (result != null)
            {
                result.ForEach(x => x.OrderStatus = data.OrderStatus);
                Context.SaveChanges();
                obj.result = true;
                obj.Message = Messages.InsertSuccess;
            }
            else
            {
                obj.result = false;
                obj.Message = Messages.SomethingWentWrong;
            }
            return obj;
        }

        public async Task<object> UpdateOrderDetails(Order jsondata)
        {
            ResponseObj obj = new ResponseObj();

            var result =await Context.Order.FindAsync(jsondata.OrderId);
            if (result != null)
            {
                result.PrdId = jsondata.PrdId;
                result.SelectedQuantity = jsondata.SelectedQuantity;
                result.Modified = DateTime.UtcNow;
                Context.SaveChanges();
                obj.result = true;
                obj.Message = Messages.InsertSuccess;
            }
            else
            {
                obj.result = false;
                obj.Message = Messages.SomethingWentWrong;
            }

            return obj;
        }

       

        public async Task<object> DeleteOrder(Order jsondata)
        {
            ResponseObj obj = new ResponseObj();
            var result =await Context.Order.FindAsync(jsondata.OrderId);
            if(result!=null)
            {
                result.IsDelete = true;
                Context.SaveChanges();
                obj.result = true;
                obj.Message = Messages.DeleteSuccess;
            }
            else
            {
                obj.result = false;
                obj.Message = Messages.SomethingWentWrong;
            }

            return obj;
        }
    }
}
