using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WashAndWow.Domain.Entities.Base
{
    public abstract class ConfiguredEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public required string Name { get; set; }

        public string? CreatorID { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? UpdaterID { get; set; }
        public DateTime? LastestUpdateAt { get; set; }

        public string? DeleterID { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
