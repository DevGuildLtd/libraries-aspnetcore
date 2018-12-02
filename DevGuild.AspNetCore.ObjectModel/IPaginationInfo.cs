using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    public interface IPaginationInfo
    {
        Int32 TotalItems { get; }

        Int32 TotalPages { get; }

        Int32 ItemsPerPage { get; }

        Int32 CurrentPage { get; }
    }
}
