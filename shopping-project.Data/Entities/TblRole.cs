using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblRole")]
public partial class TblRole
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
