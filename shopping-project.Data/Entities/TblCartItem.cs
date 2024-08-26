using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblCartItem")]
[Index("CartId", "ProductId", Name = "UC_CartId_ProductId", IsUnique = true)]
public partial class TblCartItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int CartId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("TblCartItems")]
    public virtual TblCart Cart { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("TblCartItems")]
    public virtual TblProduct Product { get; set; } = null!;
}
