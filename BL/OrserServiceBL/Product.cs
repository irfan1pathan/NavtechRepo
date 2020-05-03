using Newtonsoft.Json;
using OrderManagementSystem.BL.IOrderServiceBL;
using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.OrserServiceBL
{
    public class Product:IProducts
    {
        private readonly navtechContext Context;

        public Product(navtechContext context)
        {
            this.Context = context;
        }
        public object GetProductData()
        {
            var result = Context.Products.Where(x=>x.IsDelete==false).ToList();
            return result;
        }
        public async Task<object> UpdateProductDetils(Products jsondata, string image)
        {

            var result = await Context.Products.FindAsync(jsondata.PrdId);
            ResponseObj res = new ResponseObj();
            byte[] fs = File.ReadAllBytes(image);
            if (result != null)
            {
                result.ProductName = jsondata.ProductName;
                result.ProductHeight = jsondata.ProductHeight;
                result.Productprice = jsondata.Productprice;
                result.ProductWeight = jsondata.ProductWeight;
                result.Image = fs;
                result.AvailbleQty = jsondata.AvailbleQty;
            }
            var save = Context.SaveChanges();
            if (save == 1)
            {
                res.result = true;
                res.Message = Messages.UpdateSuccess;
            }
            else
            {
                res.result = false;
                res.Message = Messages.SomethingWentWrong;
            }
            return res;
        }


        public async Task<object> AddProductDetails(Products jsondata, string image)
        {
            ResponseObj res = new ResponseObj();
            byte[] fs = File.ReadAllBytes(image);
            Products obj = new Products
            {
                ProductName = jsondata.ProductName,
                ProductHeight = jsondata.ProductHeight,
                Productprice = jsondata.Productprice,
                ProductWeight = jsondata.ProductWeight,
                Image = fs,
                AvailbleQty = jsondata.AvailbleQty
            };
            await Context.AddAsync(obj);
            var result = Context.SaveChanges();
            if (result == 1)
            {
                res.result = true;
                res.Message = Messages.InsertSuccess;
            }
            else
            {
                res.result = false;
                res.Message = Messages.SomethingWentWrong;
            }
            return obj;

        }

        public async Task<object> DeleteProductDetails(Products jsondata)
        {
            ResponseObj obj = new ResponseObj();
            var result = await Context.Products.FindAsync(jsondata.PrdId);
            if (result != null)
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
