using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblUser")]
[Index("Tell", Name = "IX_TblUser_Tell")]
public partial class TblUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(16)]
    public string Tell { get; set; } = null!;

    [Column("isVerified")]
    public bool IsVerified { get; set; }

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("TblUsers")]
    public virtual TblRole Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual TblAddress? TblAddress { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TblCart> TblCarts { get; set; } = new List<TblCart>();

    [InverseProperty("User")]
    public virtual ICollection<TblRating> TblRatings { get; set; } = new List<TblRating>();
}
