using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using shopping_project.Data.Entities;
using shopping_project.Data.Entities.ViewModels;
using shopping_project_api.services;
using System.ComponentModel;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
namespace shopping_project_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShoppingContext _context;
        public UserController()
        {
            _context = new ShoppingContext();
        }

        [HttpGet("GetAll/{pageNo}")]
        public async Task<IActionResult> GetAll(int pageNo)
        {
            if(pageNo > 0)
            {
                var users = await _context.TblUsers.Skip((pageNo -1) *100).Take(100).ToListAsync();
                if(users.Count > 0)
                    return Ok(users);

                return NoContent();
            }
            return BadRequest();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.TblUsers.SingleOrDefaultAsync(x => x.Id == id);
            if (user is null)
            {
                return BadRequest("User with this info not found");
            }
            return Ok(user);
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> Add(User user)
        {
            if(user is null && _context.TblUsers.Any(i => i.Tell == user.Tell))
            {
                return BadRequest("User dont have Conditions to add");
            }
            else
            {
                try
                {
                    TblUser userToAdd = new TblUser()
                    {
                        Tell = user.Tell,
                        IsVerified = user.isVerified,
                        RoleId = user.RoleId,
                    };

                    await _context.TblUsers.AddAsync(userToAdd);
                    await _context.SaveChangesAsync();
                    return Ok("User Added Successfully");
                }
                catch
                {
                    return StatusCode(500, "Error retrieving users from database");
                }
            }
        }

        [HttpPost("EditUser")]
        public async Task<IActionResult> Edit(User user)
        {
            var selectedUser = await _context.TblUsers.SingleOrDefaultAsync(i => i.Id == user.Id);
            if (selectedUser is null)
                return BadRequest("user not founded");

            selectedUser.Tell = user.Tell;
            selectedUser.IsVerified = user.isVerified;
            selectedUser.RoleId = user.RoleId;
            try
            {
                _context.Entry(selectedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Another user has updated this record.");
            }
        }

        [HttpGet("VerfiedUsers/{pageNo}")]
        public async Task<ActionResult<List<TblVerifiedUser>>> GetVerified(int pageNo)
        {
            try
            {
                var users = await _context.TblVerifiedUsers.Skip(pageNo - 1 * 100).Take(100).ToListAsync();
                if (users.Count > 0)
                    return Ok(users);

                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Error retrieving users from database");
            }
        }

    }
}
