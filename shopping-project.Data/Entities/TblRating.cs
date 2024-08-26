using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblRating")]
public partial class TblRating
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public int Stars { get; set; }

    [StringLength(1024)]
    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TblRatings")]
    public virtual TblProduct Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TblRatings")]
    public virtual TblUser User { get; set; } = null!;
}
