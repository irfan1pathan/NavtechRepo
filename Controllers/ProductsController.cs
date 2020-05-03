using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.BL;
using OrderManagementSystem.BL.IOrderServiceBL;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProducts products;

        public ProductsController(IProducts products)
        {
            this.products = products;
        }

        [HttpGet]
        [Authorize(Roles =UserRoles.Admin+","+ UserRoles.User)]
        [Route("GetData")]
        public IActionResult GetProductDetails()
        {
            var entity = products.GetProductData();
            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("AddProductDetails")]

        public async Task<IActionResult> AddProductDetails([FromBody]Products data, string json)
        {
            var result = await products.AddProductDetails(data, json);
            return Ok(result);
        }


        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("UpdateProductDetails")]

        public async Task<IActionResult> UpdateProductDetails([FromBody]Products data, string json)
        {
            var result = await products.UpdateProductDetils(data, json);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("DeleteProductDetails")]

        public async Task<IActionResult> DeleteProductDetails([FromBody]Products data)
        {
            var result = await products.DeleteProductDetails(data);
            return Ok(result);
        }
    }
}