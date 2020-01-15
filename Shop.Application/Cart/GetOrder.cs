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
    public class GetOrder
    {
        public ISession _session;
        public ApplicationDbContext _ctx;

        public GetOrder(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }
        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation CustomerInformation{ get; set; }
            public int GetTotalCharge() => Products.Sum(x => x.Price * x.Qty);
        }
        public class Product
        {

            public int ProductId { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
            public int Price { get; set; }

        }
        public Response Do()
        {
            var cart = _session.GetString("cart");
            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);
            var listOfProducts = _ctx.Stock
                .Include(x => x.Product)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Price = (int)(x.Product.Price * 100),
                    Qty = cartList.FirstOrDefault(y => y.StockId == x.Id).Qty
                }).ToList();

            var customerInfoString= _session.GetString("customer-info");
            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoString);

            return new Response 
            { 
                Products=listOfProducts,
                CustomerInformation= new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    City = customerInformation.City,
                    PostCode = customerInformation.PostCode,
                }

            };
        }
    }
}