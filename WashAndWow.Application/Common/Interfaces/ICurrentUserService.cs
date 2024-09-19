﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wash_Wow.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
