using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL
{
    internal class Constants
    {
        internal static readonly string sp_productDetails = "exec sp_productDetails @jsondata";
        internal static readonly string sp_AddOrderDetails = "exec AddOrder @jsondata";
        internal static readonly string UpdateOrderDetails = "exec UpdateOrderDetails @jsondata";
        internal static readonly string sp_AdminProductDetails = "exec sp_AdminproductDetails";
    }

    internal class StoredProcedureParams
    {
        internal static readonly string Jsondata = @"jsondata";
    }

    internal class Messages
    {
        internal static readonly string InsertSuccess = "Successfully Added";
        internal static readonly string UpdateSuccess = "Successfully Updated";
        internal static readonly string SomethingWentWrong = "Something went wrong";
        internal static readonly string DeleteSuccess = "Successfully Deleted";
    }

    public class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }

}
