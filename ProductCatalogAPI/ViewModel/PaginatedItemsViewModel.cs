using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.ViewModel
{
    public class PaginatedItemsViewModel
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; } //count of records returned
        public long Count { get; set; } //total records in our db
        public IEnumerable<CatalogItem> Data { get; set; }
    }
}
