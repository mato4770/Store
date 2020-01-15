﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        public ISession _session;
        public ApplicationDbContext _ctx;

        public GetCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }
        public class Response
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
        public IEnumerable<Response> Do()
        {
            var stringObject = _session.GetString("cart");
            //return empty 
            if(string.IsNullOrEmpty(stringObject))
            {
                return new List<Response>();
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            var response = _ctx.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId ==x.Id ))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Price = $"€ {x.Product.Price.ToString("N2")}",
                    StockId= x.Id,
                    Qty=cartList.FirstOrDefault(y => y.StockId==x.Id).Qty
                })
                .ToList();

            return response;
        }
    }
}