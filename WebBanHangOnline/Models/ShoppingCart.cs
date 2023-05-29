using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { set; get; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item,int Quantity)
        {
            var checkExisted = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExisted != null)
            {
                checkExisted.Quantity += Quantity;
                checkExisted.TotalPrice = checkExisted.Price * checkExisted.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int id)
        {
            var checkExisted = Items.SingleOrDefault(x => x.ProductId == id);
            if(checkExisted != null)
            {
                Items.Remove(checkExisted);
            }
        }
        public void UpdateCart(int id, int Quantity)
        {
            var checkExisted = Items.SingleOrDefault(x => x.ProductId == id);
            if(checkExisted != null)
            {
                checkExisted.Quantity = Quantity;
                checkExisted.TotalPrice = checkExisted.Quantity * checkExisted.Price;
            }
        }
        public decimal GetToTal()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }
    public class ShoppingCartItem
    {
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string CategoryProductName { set; get; }
        public string ProductImage { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public decimal TotalPrice { set; get; }
        public string Alias { set; get; }

    }
}