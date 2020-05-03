using OrderManagementSystem.BL.IOrderServiceBL;
using OrderManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementSystem.BL.OrserServiceBL
{
    public class User:IUser
    {
        private readonly navtechContext user;

        public User(navtechContext user)
        {
            this.user = user;
        }

        public async Task<object> AddAllUsers(Users data)
        {
            ResponseObj obj = new ResponseObj();
            Users userdata =new Users()
            {
               UserName=data.UserName,
               Country=data.Country,
               State=data.State,
               Zip=data.Zip,
               Age=data.Age,
               Gender=data.Gender,
               Role=data.Role,
               Password=data.Password
            };
            await user.AddAsync(obj);
            var result=user.SaveChanges();
            if(result==1)
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

        public async Task<object> DeleteUserData(int i)
        {
            ResponseObj obj = new ResponseObj();
            var result =await user.Users.FindAsync(i);
            if(result!=null)
            {
                result.IsDelete = true;
                user.SaveChanges();
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

        public object GetAllUsers()
        {
            var result = user.Users.ToList();
            return result;
        }

        public async Task<object> UpdateAllUsers(Users data)
        {
            ResponseObj obj=new ResponseObj();
            var result =await user.Users.FindAsync(data.Uid);
            if (result != null)
            {
                result.UserName = data.UserName;
                result.Age = data.Age;
                result.City = data.City;
                result.Country = data.Country;
                result.Gender = data.Gender;
                result.Role = data.Role;
                result.State = data.State;
                result.Address = data.Address;
                result.ModifiedDate = DateTime.Now;
                user.SaveChanges();
                obj.result = true;
                obj.Message = Messages.UpdateSuccess;
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
