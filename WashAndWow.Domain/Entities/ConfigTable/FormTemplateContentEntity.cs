using System.ComponentModel.DataAnnotations.Schema;
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
