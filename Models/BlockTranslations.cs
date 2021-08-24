using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين الترجمات المختلفة للكتل
    /// </summary>
    public class BlockTranslations
    {
        /// <summary>
        /// معرف ترجمة الكتلة
        /// </summary>
        [Key]
        public int BlockTranslationId { get; set; }

        /// <summary>
        /// اسم الكتلة باللغة المختارة
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// الوصف المترجم للكتلة
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// رقم الكتلة المرتبطة معها الترجمة
        /// </summary>
        [Display(Name = "Block")] 
        [ForeignKey("Block")]
        public int BlockId { get; set; }

        /// <summary>
        /// لتفعيل أو إلغاء تفعيل ترجمة الكتلة
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// لتخزين رمز لغة الترجمة
        /// </summary>
        [Display(Name = "Language")]
        public string Language { get; set; }

        /// <summary>
        /// لترميز ترجمة الكتلة
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

        public Blocks Block { get; set; }
    }
}
