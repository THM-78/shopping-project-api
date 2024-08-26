using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("Tbl Address")]
[Index("UserId", Name = "IX_Tbl Address_UserId", IsUnique = true)]
public partial class TblAddress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Address { get; set; } = null!;

    [StringLength(16)]
    public string City { get; set; } = null!;

    [StringLength(32)]
    public string State { get; set; } = null!;

    [StringLength(20)]
    public string? PostalCode { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("TblAddress")]
    public virtual TblUser User { get; set; } = null!;
}
