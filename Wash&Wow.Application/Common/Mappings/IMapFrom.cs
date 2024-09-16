using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wash_Wow.Application.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
