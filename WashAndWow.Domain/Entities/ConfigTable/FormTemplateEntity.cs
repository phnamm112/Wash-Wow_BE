using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities.ConfigTable
{
    [Table("FormTemplate")]
    public class FormTemplateEntity : ConfiguredEntity
    {        
        public virtual ICollection<FormTemplateContentEntity> FormTemplateContents {  get; set; }
    }
}
