using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities.ConfigTable;

namespace WashAndWow.Domain.Entities
{
    [Table("FormFieldValue")]
    public class FormFieldValueEntity : BaseEntity
    {
        public required string FormID { get; set; }
        [ForeignKey(nameof(FormID))]
        public virtual FormEntity Form { get; set; }

        public required int FormTemplateContentID { get; set; } // Trỏ tới content field
        [ForeignKey(nameof(FormTemplateContentID))]
        public virtual FormTemplateContentEntity FormTemplateContent { get; set; }

         public required string FieldValue { get; set; } // Giá trị điền vào
    }
}
