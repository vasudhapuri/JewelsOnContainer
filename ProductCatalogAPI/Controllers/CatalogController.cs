﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogAPI.Data;
using ProductCatalogAPI.Domain;
using ProductCatalogAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Items([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 6)
        {
            var itemsCount = _context.Catalog.LongCountAsync(); //itemsCount is task of long type
                                                                //ie. it is a thread
            var items = await _context.Catalog
                  .OrderBy(c => c.Name)
                  .Skip(pageIndex * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

            items = ChangePictureUrl(items);
            var model = new PaginatedItemsViewModel
            {
                pageIndex = pageIndex,
                pageSize = items.Count,
                Count = itemsCount.Result ,
                Data = items
        };

            return Ok(model);
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> items)
        {
            items.ForEach(item =>
            item.PictureUrl=item.PictureUrl.Replace
            ("http://externalcatalogbaseurltobereplaced", _config["ExternalCatalogUrl"]));
            return items;
        }
    }
}
