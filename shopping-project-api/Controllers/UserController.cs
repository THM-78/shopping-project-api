using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using shopping_project.Data.Entities;
using shopping_project_api.Data.Models.ViewModels;
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
        private readonly IMapper AutoMapper;
        public UserController(IMapper mapper)
        {
            _context = new ShoppingContext();
            AutoMapper = mapper;
        }

        [HttpGet("GetAll/{pageNo}")]
        public async Task<IActionResult> GetAll(int pageNo)
        {
            var users = await _context.TblUsers.Skip(pageNo -1 *100).Take(100).ToListAsync();
            if(users.Count > 0)
                return Ok(users);

            return NoContent();
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
        public async Task<IActionResult> Add(TblUser user)
        {
            if(user is null && _context.TblUsers.Any(i => i.Tell == user.Tell))
            {
                return BadRequest("User dont have Conditions to add");
            }
            else
            {
                try
                {
                    await _context.TblUsers.AddAsync(user);
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
        public async Task<IActionResult> Edit(TblUser user)
        {
            var selectedUser = await _context.TblUsers.SingleOrDefaultAsync(i => i.Id == user.Id);
            if (selectedUser is null)
                return BadRequest("user not founded");

            AutoMapper.Map(user, selectedUser);
            try
            {
                // ذخیره تغییرات با استفاده از قفل خوش‌بینانه
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
