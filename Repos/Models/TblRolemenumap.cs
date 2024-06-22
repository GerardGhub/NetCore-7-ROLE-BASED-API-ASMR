﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnAPI.Repos.Models;

[Table("tbl_rolemenumap")]
public partial class TblRolemenumap
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("userrole")]
    [StringLength(20)]
    public string Userrole { get; set; } = null!;

    [Column("menucode")]
    [StringLength(50)]
    public string Menucode { get; set; } = null!;
}
