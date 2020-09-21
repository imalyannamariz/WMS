using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS.Models.Category
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage ="Category Name is required")]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Category Description is required")]
        [DisplayName("Category Description")]
        public string CategoryDescription { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public int mode { get; set; }
        public string msg { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Inventory> Inventories { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}