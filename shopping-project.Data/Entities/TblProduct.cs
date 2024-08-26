using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblProduct")]
public partial class TblProduct
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(64)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(12, 0)")]
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public int? Stock { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TblProducts")]
    public virtual TblCategory? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<TblCartItem> TblCartItems { get; set; } = new List<TblCartItem>();

    [InverseProperty("Product")]
    public virtual ICollection<TblImage> TblImages { get; set; } = new List<TblImage>();

    [InverseProperty("Product")]
    public virtual ICollection<TblRating> TblRatings { get; set; } = new List<TblRating>();
}
