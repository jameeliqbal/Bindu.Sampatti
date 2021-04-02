using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Bindu.Sampatti.Web.Pages.AssetManagement.AssetClass
{
    public class CreateModalModel : PageModel
    {
        [BindProperty]
        public CreateAssetClassViewModal NewClass { get; set; }
        private static SelectListGroup buildingGroup = new SelectListGroup()
        {
            Name = "A. Building"
        };
        private static SelectListGroup PMGroup = new SelectListGroup()
        {
            Name = "B. Plant & Machinery"
        };
        private static SelectListGroup computersGroup = new SelectListGroup()
        {
            Name = "E. Computers"
        };
        public List<SelectListItem> parentClasses { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){  Value="0", Text=""},
            //new SelectListItem(){ Value="A", Text="A. Building", Group=buildingGroup },
            new SelectListItem(){ Value="A1", Text="A.1. Borewell & Land Development", Group=buildingGroup},
            //new SelectListItem(){ Value="B", Text="B. Plant & Machinery", Group=buildingGroup},
            new SelectListItem(){ Value="B1", Text="B.1.Production Machinery", Group=PMGroup},
            new SelectListItem(){ Value="B11", Text="B.1.1. Component 1", Group=PMGroup},
            new SelectListItem(){ Value="B12", Text="B.1.2. Component 2", Group=PMGroup},
            new SelectListItem(){ Value="B13", Text="B.1.3. Component 3", Group=PMGroup},
            //new SelectListItem(){ Value="E", Text="E. Computers", Group=computersGroup},
            new SelectListItem(){ Value="E1", Text="E.1. Laptop", Group=computersGroup},
            new SelectListItem(){ Value="E2", Text="E.2. CPU", Group=computersGroup},
            new SelectListItem(){ Value="E3", Text="E.3. Monitor", Group=computersGroup},
            new SelectListItem(){ Value="E4", Text="E.4. Printer", Group=computersGroup},
        };
        public void OnGet()
        {
        }

        public class CreateAssetClassViewModal
        {
            public string Code { get; set; }
            public string Name { get; set; }
            [SelectItems(nameof(parentClasses))]
            [Display(Name="Parent Class")]
            public string ParentClass { get; set; }
            
            [Display(Name = "Is a Component?")]
            public bool IsComponent { get; set; }
        }

        public enum TypeOfAsset
        {
            Class,
            Component
        }
    }
}
