//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueprintMediaServis.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class BoardMembers
    {
        public int id { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public string name_tr { get; set; }
        public string position_tr { get; set; }
        public byte[] image_tr { get; set; }
        [AllowHtml]
        public string content_tr { get; set; }
        public string name_en { get; set; }
        public string position_en { get; set; }
        public byte[] image_en { get; set; }
        [AllowHtml]
        public string content_en { get; set; }
        public string name_ru { get; set; }
        public string position_ru { get; set; }
        public byte[] image_ru { get; set; }
        [AllowHtml]
        public string content_ru { get; set; }
        public string imageName_tr { get; set; }
        public string imageName_en { get; set; }
        public string imageName_ru { get; set; }
    }
}
