using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblImage")]
[Index("ImgUrl", Name = "IX_TblImage_ImgUrl")]
public partial class TblImage
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int ProductId { get; set; }

    [StringLength(64)]
    public string ImgUrl { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("TblImages")]
    public virtual TblProduct Product { get; set; } = null!;
}
