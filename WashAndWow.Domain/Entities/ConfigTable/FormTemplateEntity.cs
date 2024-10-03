using System.ComponentModel.DataAnnotations.Schema;
using WashAndWow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities.ConfigTable
{
    [Table("FormTemplate")]
    public class FormTemplateEntity : ConfiguredEntity
    {
        public virtual ICollection<FormTemplateContentEntity> FormTemplateContents { get; set; }
    }
}
