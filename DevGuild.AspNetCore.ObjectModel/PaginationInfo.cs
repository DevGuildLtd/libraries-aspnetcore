using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.ObjectModel
{
    public class PaginationInfo : IPaginationInfo
    {
        public PaginationInfo(Int32 totalItems, Int32 totalPages, Int32 itemsPerPage, Int32 currentPage)
        {
            this.TotalItems = totalItems;
            this.TotalPages = totalPages;
            this.ItemsPerPage = itemsPerPage;
            this.CurrentPage = currentPage;
        }

        public Int32 TotalItems { get; }

        public Int32 TotalPages { get; }

        public Int32 ItemsPerPage { get; }

        public Int32 CurrentPage { get; }
    }
}
