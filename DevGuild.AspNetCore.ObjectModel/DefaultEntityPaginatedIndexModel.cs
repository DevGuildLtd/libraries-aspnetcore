using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    public class DefaultEntityPaginatedIndexModel<T> : IEntityPaginatedIndexModel<T>
    {
        public PaginationResult<T> Items { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
