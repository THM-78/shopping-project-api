using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shopping_project.Data.Entities.ViewModels
{
    public class User
    {

        public int Id { get; set; }
        [Required]
        public string Tell { get; set; }
        public bool isVerified { get; set; }
        public int RoleId { get; set; }

        [JsonConstructor]
        public User(int id, string tell, bool isVerified, int roleId)
        {
            Id = id;
            Tell = tell;
            this.isVerified = isVerified;
            RoleId = roleId;
        }

        public User(TblUser user)
        {
            Id = user.Id;
            Tell = user.Tell;
            isVerified = user.IsVerified;
            RoleId = user.RoleId;
        }
        
    }
}
