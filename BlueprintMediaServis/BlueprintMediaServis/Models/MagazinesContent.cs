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
    
    public partial class MagazinesContent
    {
        public int id { get; set; }
        public int magazineId { get; set; }
        public Nullable<System.DateTime> createTime { get; set; }
        public Nullable<System.DateTime> updateTime { get; set; }
        public string pdfName_tr { get; set; }
        public string imageName_tr { get; set; }
        public string title_tr { get; set; }
        public byte[] pdfFile_tr { get; set; }
        public byte[] imageFile_tr { get; set; }
        public string imageName_en { get; set; }
        public string pdfName_en { get; set; }
        public string title_en { get; set; }
        public byte[] pdfFile_en { get; set; }
        public byte[] imageFile_en { get; set; }
        public string imageName_ru { get; set; }
        public string pdfName_ru { get; set; }
        public string title_ru { get; set; }
        public byte[] pdfFile_ru { get; set; }
        public byte[] imageFile_ru { get; set; }
        public Nullable<int> categoryId { get; set; }
    
        public virtual Magazines Magazines { get; set; }
        public virtual MagazineCategory MagazineCategory { get; set; }
    }
}
