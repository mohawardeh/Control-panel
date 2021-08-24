using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين فئات الأحداث 
    /// </summary>
    public class EventCategories
    {
        /// <summary>
        /// معرف فئة الحدث
        /// </summary>
        [Key]
        public int EventCategoryId { get; set; }

        /// <summary>
        /// اسم فئة الحدث 
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// وصف فئة الحدث
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// مسار الصورة المرتبطة مع هذه الفئة
        /// </summary>
        [Display(Name = "Image")]
        public string Image { get; set; }

        /// <summary>
        /// لتفعيل أو إلغاء تفعيل الفئة
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// ترتيب فئة الحدث
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

        public ICollection<Events> Events { get; set; }
        public ICollection<EventCategoryTranslations> EventCategoryTranslations { get; set; }
    }
}
