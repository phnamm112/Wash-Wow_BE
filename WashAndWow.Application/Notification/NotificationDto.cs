using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Mappings;
using Wash_Wow.Domain.Entities;
using WashAndWow.Application.LaundryShops;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Application.Notification
{
    public class NotificationDto : IMapFrom<NotificationEntity>
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public string ReceiverName { get; set; }
        public string Status { get; set; } // "Read" or "Unread"
        public DateTime? ReadAt { get; set; }
        public string Type { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<NotificationEntity, NotificationDto>();
    }
}
}
