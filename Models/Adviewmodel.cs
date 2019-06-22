using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplicationEmarketing.Models
{
    public class Adviewmodel
    {
        public int Pro_id{ get; set; }
        public string Pro_Name { get; set; }
        public string pro_image { get; set; }
        public string Pro_descreption { get; set; }
        public Nullable<int> pro_price { get; set; }
        public Nullable<int> pro_fk_cat { get; set; }
        public Nullable<int> pro_fk_user { get; set; }
        public int cat_id { get; set; }
        public string cat_Name { get; set; }
        public string U_Name { get; set; }
        public string U_image { get; set; }
        public string U_contact { get; set; }
    }
}