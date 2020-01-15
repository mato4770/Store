using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shop.Database;
using Shop.Domain.Models;
using Shop.Application.ProductsAdmin;

namespace Store.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private ApplicationDbContext _ctx;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        public IEnumerable<Shop.Application.Products.GetProducts.ProductViewModel> Products { get; set; }
        public void OnGet()
        {
            Products =new Shop.Application.Products.GetProducts(_ctx).Do();
        }
     
    }
}
