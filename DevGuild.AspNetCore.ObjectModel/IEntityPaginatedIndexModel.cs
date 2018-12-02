using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    public interface IEntityPaginatedIndexModel<T> : IEnumerable<T>
    {
        PaginationResult<T> Items { get; set; }
    }
}
