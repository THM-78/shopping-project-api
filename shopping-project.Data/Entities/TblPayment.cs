using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace shopping_project.Data.Entities;

[Table("TblPayment")]
public partial class TblPayment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
}
