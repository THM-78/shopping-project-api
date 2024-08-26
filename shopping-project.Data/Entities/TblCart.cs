using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblCart")]
public partial class TblCart
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int UserId { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<TblCartItem> TblCartItems { get; set; } = new List<TblCartItem>();

    [ForeignKey("UserId")]
    [InverseProperty("TblCarts")]
    public virtual TblUser User { get; set; } = null!;
}
