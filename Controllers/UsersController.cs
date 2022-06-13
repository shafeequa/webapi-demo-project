using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebApiDemoProject.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private static string jsonFile = @"C:\Users\Windows\source\repos\WebApiDemoProject\userslist.json";


        // GET: api/<UsersController>
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        public List<User> Get()
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
            return userList;
        }

        // GET api/<UsersController>/5
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
            User user = userList.Where(u => u.Id == id).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("user does not exist");
            }
        }

        // POST api/<UsersController>
        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        public void Post([FromBody] User user)
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
            User newUser = new User()
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                MobileNumber = user.MobileNumber,
                Address = user.Address,
                EmailId = user.EmailId,
                Role = user.Role
            };
            userList.Add(newUser);
            System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(userList));
        }

        // PUT api/<UsersController>/5
        [EnableCors("AllowSpecificOrigin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            var json = System.IO.File.ReadAllText(jsonFile);
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
            User getUser = userList.Where(u => u.Id == id).FirstOrDefault();
            if (getUser != null)
            {
                getUser.Id = id;
                getUser.Firstname = user.Firstname;
                getUser.Lastname = user.Lastname;
                getUser.MobileNumber = user.MobileNumber;
                getUser.Address = user.Address;
                getUser.EmailId = user.EmailId;
                getUser.Role = user.Role;
                System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(userList));
            }
            else
            {
                throw new Exception("user with this id does not exist!");
            }
        }

        // DELETE api/<UsersController>/5
        [EnableCors("AllowSpecificOrigin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            var json = System.IO.File.ReadAllText(jsonFile);
            List<User> userList = JsonConvert.DeserializeObject<List<User>>(json);
            User getUser = userList.Where(u => u.Id == id).FirstOrDefault();
            if (getUser != null)
            {
                userList.Remove(getUser);
                System.IO.File.WriteAllText(jsonFile, JsonConvert.SerializeObject(userList));
            }
            else
            {
                throw new Exception("user not found");
            };

        }
    }
}
