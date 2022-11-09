﻿using AssetManager.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.DTOs;

public class AssetAddEditDto
{
    public int AssetId { get; set; }
    [Required]
    [DisplayName("Asset Type")]
    public AssetType AssetType { get; set; }
    [Required]
    public Product Model { get; set; }
    [Required]
    public Site Site { get; set; }
    public int? PersonId { get; set; }
}