using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// لتخزين كتل الموقع المختلفة
    /// </summary>
    public class Blocks
    {
        /// <summary>
        /// معرف الكتلة
        /// </summary>
        [Key]
        public int BlockId { get; set; }

        /// <summary>
        /// اسم الكتلة 
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// وصف الكتلة
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// مفتاح خارجي مع نمط الكتلة
        /// </summary>
        [Display(Name = "Category")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// لإظهار أو إخفاء الكتلة
        /// </summary>
        [Display(Name = "Is Visible")]
        public bool IsVisible { get; set; }

        /// <summary>
        /// لتخزين مسار الصورة المرتبطة مع الكتلة
        /// </summary>
        [Display(Name = "Image")]
        public string Image { get; set; }

        /// <summary>
        /// لتخزن العنوان المرتبط مع الكتلة
        /// </summary>
        [Display(Name = "Url")]
        public string Url { get; set; }

        /// <summary>
        /// لتخزين مسار الملف المرتبط مع الكتلة
        /// </summary>
        [Display(Name = "File")]
        public string File { get; set; }

        /// <summary>
        /// لتفعيل أو إلغاء تفعيل الكتلة
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        /// <summary>
        /// ترتيب الكتلة
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

        public BlocksCategories Category { get; set; }
        public ICollection<BlockTranslations> BlockTranslations { get; set; }
    }
}
