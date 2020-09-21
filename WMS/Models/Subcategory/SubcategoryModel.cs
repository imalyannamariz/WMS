using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMS.Models.Subcategory
{
    public class SubcategoryModel
    {
        public int SubCategoryID { get; set; }
        [Required(ErrorMessage = "Subcategory Name is required")]
        [DisplayName("Subcategory Name")]
        public string SubCategoryName { get; set; }
        [Required(ErrorMessage = "Subcategory Description is required")]
        [DisplayName("Subcategory Description")]
        public string SubCategoryDescription { get; set; }


        [Required(ErrorMessage = "Category Name is required")]
        [DisplayName("Category Name")]
        public int CategoryID { get; set; }
        //public List<SelectListItem> CategoryList { get; set; }
        //public int SelectedCategoryID{ get; set; }
        //public string SelectedCategoryName { get; set; }

        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedDate { get; set; }
        public int mode { get; set; }
        public string msg { get; set; }
    }
}