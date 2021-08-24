using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين الأحداث المختلفة
    /// </summary>
    public class Events
    {
        /// <summary>
        /// معرف الحدث
        /// </summary>
        [Key]
        public int EventId { get; set; }

        /// <summary>
        /// اسم الحدث 
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// مفتاح خارجي مع نمط الأحداث
        /// </summary>
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// تاريخ بداية الحدث
        /// </summary>
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// تاريخ نهاية الحدث
        /// </summary>
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// لتخزين مسار الصورة المرتبطة مع الحدث
        /// </summary>
        [Display(Name = "Image")]
        public string Image { get; set; }

        /// <summary>
        /// لتخزين مسار الملف المرتبط مع الحدث
        /// </summary>
        [Display(Name = "File")]
        public string File { get; set; }

        /// <summary>
        /// لتفعيل أو إلغاء تفعيل الحدث
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// ترتيب الحدث
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

        public EventCategories Category { get; set; }
        public ICollection<EventTranslations> EventCategoryTranslations { get; set; }
    }
}
