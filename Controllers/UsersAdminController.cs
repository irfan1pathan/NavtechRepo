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
    public class UsersAdminController : ControllerBase
    {
        private readonly IUser context;

        public UsersAdminController(IUser context)
        {
            this.context = context;
        }
        [HttpGet]
        [Authorize(Roles =UserRoles.Admin)]
        [Route("GetAllUsers")]
        public IActionResult GetUsersData()
        {
            var result = context.GetAllUsers();
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("AddUsers")]
        public IActionResult AddUsersData([FromBody]Users data)
        {
            var result = context.AddAllUsers(data);
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("UpdateUsers")]
        public IActionResult UpdateUsersData([FromBody]Users data)
        {
            var result = context.UpdateAllUsers(data);
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("DeleteUsers")]
        public IActionResult DeleteUsers([FromBody]int i)
        {
            var result = context.DeleteUserData(i);
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }
    }
}