using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;

namespace WashAndWow.Domain.Entities
{
    [Table("FormImage")]
    public class FormImageEntity : BaseEntity
    {
        public required string Url { get; set; }
        public required string FormID { get; set; }
        [ForeignKey(nameof(FormID))]
        public virtual FormEntity Form {  get; set; }
    }
}
