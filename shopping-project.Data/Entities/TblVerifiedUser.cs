using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

public partial class TblVerifiedUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [StringLength(32)]
    public string Username { get; set; } = null!;

    [StringLength(64)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(16)]
    public string Tell { get; set; } = null!;
}
