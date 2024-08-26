using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblCategory")]
public partial class TblCategory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<TblProduct> TblProducts { get; set; } = new List<TblProduct>();
}
