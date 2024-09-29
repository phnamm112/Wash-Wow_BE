using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Entities.Base;
using WashAndWow.Domain.Entities.ConfigTable;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Domain.Entities
{
    [Table("Form")]
    public class FormEntity : BaseEntity
    {
        public required FormStatus Status { get; set; }

        [ForeignKey(nameof(CreatorID))]
        public virtual UserEntity Sender { get; set; }

        public required int FormTemplateID {  get; set; }
        [ForeignKey(nameof(FormTemplateID))]
        public virtual FormTemplateEntity FormTemplate { get; set; }

        public virtual ICollection<FormImageEntity> FormImages { get; set; }
        public virtual ICollection<FormFieldValueEntity> FieldValues { get; set; }
    }
}
