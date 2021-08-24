using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين الملفات المرفوعة
    /// </summary>
    public class Uploads
    {
        /// <summary>
        /// معرف الملف
        /// </summary>
        [Key]
        public int UploadId { get; set; }

        /// <summary>
        /// اسم الملف
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// الوصف الملف
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// mime type لاحقة الملف أو 
        /// </summary>
        [Display(Name = "Extension")]
        public string Extension { get; set; }

        /// <summary>
        /// مسار الملف على القرص الصلب
        /// </summary>
        [Display(Name = "File Url")]
        public string FileUrl { get; set; }

        /// <summary>
        /// رقم مفتاح العنصر(س) التابع له الملف
        /// </summary>
        [Display(Name = "Reference Id")]
        public int ReferenceId { get; set; }

        /// <summary>
        /// (اسم الجدول الذي يرتبط فيه هذا الملفوالذي يتبع له العنصر (س
        /// </summary>
        [Display(Name = "Reference")]
        public string Reference { get; set; }


        /// <summary>
        /// العنوان المرتبط بالملف
        /// </summary>
        [Display(Name = "Url")]
        public string Url { get; set; }

        /// <summary>
        /// (Image, Video, File ) نوع الملف
        /// </summary>
        [Display(Name = "Type")]
        public string Type { get; set; }

        /// <summary>
        /// لترتيب العنصر
        /// </summary>
        [Display(Name = "Record Order")]
        public int RecordOrder { get; set; }

        /// <summary>
        /// معرف المستخدم المنشئ
        /// </summary>
        [Display(Name = "Insert UserName")]
        public int? InsertUserId { get; set; }

        /// <summary>
        /// تاريخ الإنشاء
        /// </summary>
        [Display(Name = "Insert Date")]
        public DateTimeOffset? InsertDateTime { get; set; }

        /// <summary>
        /// تاريخ آخر تعديل
        /// </summary>
        [Display(Name = "Update Date")]
        public DateTimeOffset? UpdateDateTime { get; set; }

        /// <summary>
        /// معرف آخر مستخدم عدل السجل
        /// </summary>
        [Display(Name = "Update UserName")]
        public int? UpdateUserId { get; set; }
    }
}
