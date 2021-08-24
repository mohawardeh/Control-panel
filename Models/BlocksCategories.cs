using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coderush.Models
{
    /// <summary>
    /// تخزين الأنماط المختلفة كتل الموقع
    /// </summary>
    public class BlocksCategories
    {
        /// <summary>
        /// معرف نمط الكتلة
        /// </summary>
        [Key]
        public int BlocksCategoryId { get; set; }

        /// <summary>
        /// اسم نمط الكتلة
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// وصف نمط الكتلة
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// نوع الكتلة (blog, links, photo_Galleries,)
        /// </summary>
        [Display(Name = "Block Type")]
        public string BlockType { get; set; }

        /// <summary>
        /// تفعيل أو عدم تفعيل نمط الكتلة
        /// </summary>
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

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

        public ICollection<Blocks> Blocks { get; set; }
    }
}
