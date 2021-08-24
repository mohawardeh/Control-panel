using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين الترجمات المختلفة لفئات الأحداث
    /// </summary>
    public class EventCategoryTranslations

    {
        /// <summary>
        /// معرف ترجمة الفئة
        /// </summary>
        [Key]
        public int EventCategoryTranslationId { get; set; }

        /// <summary>
        /// اسم الفئة باللغة المختارة
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// الوصف لفئة الحدث
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// رقم قئة الحدث المرتبطة معها الترجمة
        /// </summary>
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// لتفعيل أو إلغاء تفعيل ترجمة قئة الحدث
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// لتخزين رمز لغة الترجمة
        /// </summary>
        [Display(Name = "Language")]
        public string Language { get; set; }

        /// <summary>
        /// لترميز ترجمة قئة الحدث
        /// </summary>
        [Display(Name = "Slug")]
        public string Slug { get; set; }


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
    }
}
