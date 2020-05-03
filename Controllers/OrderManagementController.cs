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
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderService context;

        public OrderManagementController(IOrderService context)
        {
            this.context = context;
        }

        [HttpPost]
        [Authorize(Roles =UserRoles.Admin+","+UserRoles.User)]
        [Route("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails([FromBody]dynamic json)
        {
            var result =await context.GetOrderDetails(json.ToString());
            return Ok(result.ToString());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.User)]
        [Route("AddOrderDetails")]

        public async Task<IActionResult> AddOrderDetails([FromBody]dynamic json)
        {
            var result = await context.AddorderDetails(json.ToString());
            return Ok(result);
        }

        
        



        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("UpdateOrderStatus")]

        public async Task<IActionResult> UpdateOrderStatus([FromBody]Order json)
        {
            var result = await context.UpdateOrderStatus(json);
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.User)]
        [Route("UpdateOrderDetails")]

        public async Task<IActionResult> UpdateOrderDetails([FromBody]Order json)
        {
            var result = await context.UpdateOrderDetails(json);
            return Ok(result);
        }

        [HttpPut]
        //[Authorize(Roles = UserRoles.User)]
        [Route("DeleteOrderDetails")]

        public async Task<IActionResult> DeleteOrderDetails([FromBody]Order json)
        {
            var result = await context.DeleteOrder(json);
            return Ok(result);
        }
    }
}