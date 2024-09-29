using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities.ConfigTable
{
    [Table("FormTemplateContent")]
    public class FormTemplateContentEntity : ConfiguredEntity
    {
        public required int FormTemplateID { get; set; }
        [ForeignKey(nameof(FormTemplateID))]
        public virtual FormTemplateEntity FormTemplate { get; set; }
    }
}
