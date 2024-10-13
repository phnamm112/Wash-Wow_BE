using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Domain.Entities;
using Wash_Wow.Domain.Repositories;
using WashAndWow.Domain.Entities;

namespace WashAndWow.Domain.Repositories
{
    public interface INotificationRepository : IEFRepository<NotificationEntity, NotificationEntity>
    {
    }
}
