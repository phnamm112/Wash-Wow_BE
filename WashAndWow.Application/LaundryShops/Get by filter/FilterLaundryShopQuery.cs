using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Application.Common.Pagination;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.LaundryShops.Get_by_filter
{
    public class FilterLaundryShopQuery : IQuery, IRequest<PagedResult<LaundryShopDto>>
    {
        public FilterLaundryShopQuery()
        {
            
        }
        public FilterLaundryShopQuery(int no, int pageSize)
        {
            PageNumber = no;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Address { get; set; }
        public string? Name { get; set; }
        public string? PhoneContact { get; set; }
        public LaundryShopStatus? Status { get; set; }
        public string? OwnerID { get; set; }

    }
}
