using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.IOrderServiceBL
{
    public interface IProducts
    {
        object GetProductData();
        Task<object> AddProductDetails(Products jsondata, string image);
        Task<object> DeleteProductDetails(Products data);
        Task<object> UpdateProductDetils(Products jsondata, string image);
    }
}
