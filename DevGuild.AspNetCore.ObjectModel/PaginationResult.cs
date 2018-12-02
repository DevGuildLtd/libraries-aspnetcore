using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    public class PaginationResult<T> : IPaginationInfo
    {
        public PaginationResult(IEnumerable<T> items, Int32 totalItems, Int32 totalPages, Int32 itemsPerPage, Int32 currentPage)
        {
            this.Items = items.ToList();
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.ItemsPerPage = itemsPerPage;
            this.CurrentPage = currentPage;
        }

        public PaginationResult(IEnumerable<T> items, IPaginationInfo info)
        {
            this.Items = items.ToList();
            this.TotalItems = info.TotalItems;
            this.TotalPages = info.TotalPages;
            this.ItemsPerPage = info.ItemsPerPage;
            this.CurrentPage = info.CurrentPage;
        }

        public IReadOnlyList<T> Items { get; }

        public Int32 TotalItems { get; }

        public Int32 TotalPages { get; }

        public Int32 ItemsPerPage { get; }

        public Int32 CurrentPage { get; }
    }
}
