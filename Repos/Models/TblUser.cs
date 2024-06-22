﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos.Models;

[Table("tbl_user")]
public partial class TblUser
{
    [Key]
    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [Column("role")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Role { get; set; }

    [Column("islocked")]
    public bool? Islocked { get; set; }

    [Column("failattempt")]
    public int? Failattempt { get; set; }
}
