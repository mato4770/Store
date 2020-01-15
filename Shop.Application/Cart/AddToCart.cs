using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        public ISession _session;

        public AddToCart(ISession session)
        {
            _session = session;
        } 
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
        public void Do(Request request)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");

            //ce je cart null nastavi na nekaj
            if(!string.IsNullOrEmpty(stringObject))
            {
                cartList= JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }
            //ce ima cart stock, najdi stock in dodaj
            if(cartList.Any(x => x. StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;
            }
            //dodaj product v cart list
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }

            
            stringObject = JsonConvert.SerializeObject(cartList);
            //add cart to session
            _session.SetString("cart", stringObject);
        }
    }
}
