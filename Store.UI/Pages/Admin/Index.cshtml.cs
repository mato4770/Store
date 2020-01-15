using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Domain.Models;

namespace Store.UI.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public Product Product { get; set; }
        public void OnGet()
        {
        }
    }
}
