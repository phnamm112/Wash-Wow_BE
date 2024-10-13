using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities.Base;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Domain.Entities
{
    [Table("Notification")]
    public class NotificationEntity : BaseEntity
    {
        public string? ReceiverID { get; set; }
        public string? Content { get; set; } 
        public bool IsRead { get; set; } 
        public DateTime? ReadAt { get; set; }
        public NotificationType Type { get; set; }
    }
}
