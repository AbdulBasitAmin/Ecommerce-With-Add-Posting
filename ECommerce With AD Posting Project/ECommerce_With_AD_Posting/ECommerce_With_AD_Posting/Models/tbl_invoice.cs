//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerce_With_AD_Posting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_invoice
    {
        public int inv_id { get; set; }
        public Nullable<int> ad_id_fk { get; set; }
        public Nullable<int> cat_id_fk { get; set; }
        public Nullable<int> u_id_fk { get; set; }
        public Nullable<int> pro_id_fk { get; set; }
        public Nullable<int> o_id_fk { get; set; }
        public Nullable<double> total_bill { get; set; }
        public Nullable<System.DateTime> inv_date { get; set; }
    
        public virtual tbl_admin tbl_admin { get; set; }
        public virtual tbl_cateogry tbl_cateogry { get; set; }
        public virtual tbl_order tbl_order { get; set; }
        public virtual tbl_product tbl_product { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
