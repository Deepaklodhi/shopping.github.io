//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplicationEmarketing.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_product
    {
        public int Pro_id { get; set; }
        public string Pro_Name { get; set; }
        public string Pro_image { get; set; }
        public Nullable<int> pro_price { get; set; }
        public string pro_descreption { get; set; }
        public Nullable<int> pro_fk_user { get; set; }
        public Nullable<int> pro_fk_cat { get; set; }
    
        public virtual tbl_category tbl_category { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
