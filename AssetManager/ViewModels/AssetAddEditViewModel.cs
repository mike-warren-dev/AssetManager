﻿using AssetManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AssetManager.ViewModels
{
    public class AssetAddEditViewModel
    {
        public Asset Asset { get; set; } = null!;
        public IEnumerable<Person> People { get; set; } = null!;
        public IEnumerable<SelectListItem> AssetTypeOptions { get; set; } = null!;
        public IEnumerable<SelectListItem> ModelOptions { get; set; } = null!;
        public IEnumerable<SelectListItem> SiteOptions { get; set; } = null!;
    }
}
