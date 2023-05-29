using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialCartView()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult PartialItemCheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.Items.Any())
            {
                return Json(new { count = cart.Items.Count },JsonRequestBehavior.AllowGet);
            }
            return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1,count =0};
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                var curprice = checkProduct.IsSale ? checkProduct.PriceSale : checkProduct.Price;
                ShoppingCartItem item = new ShoppingCartItem()
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryProductName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Price = (decimal)curprice,
                    Quantity = quantity
                };
                var image = checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault);
                if (image != null)
                    item.ProductImage = image.Image;
                item.TotalPrice = item.Price * item.Quantity;
                cart.AddToCart(item, quantity);
                Session["cart"] = cart;
                code = new { Success = true, msg = "Thêm vào giỏ hàng thành công", code = 1, count = cart.Items.Count };
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            var code = new { Success = false, msg = "", code = -1, count = cart.Items.Count };
            if(cart.Items.Count == 1)
            {
                DeleteAll();
                code = new { Success = true, msg = "Xóa giỏ hành thành công", code = 1, count = 0 };
                return Json(code);
            }

            if (cart != null && cart.Items.Any())
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct != null)
                {
                    cart.Items.Remove(checkProduct);
                    code = new { Success = true, msg = "Xóa thành công sản phẩm ra khỏi giỏ hàng", code = -1, count = cart.Items.Count };
                    Session["cart"] = cart;
                    return Json(code);
                }
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                cart.UpdateCart(checkProduct.Id, quantity);
                Session["cart"] = cart;
                code = new { Success = true, msg = "Cập vào giỏ hàng thành công", code = 1, count = cart.Items.Count };
            }
            return Json(code);
        }
        public ActionResult DeleteAll()
        {
            var code = new { Success = false, msg = "", code = -1, count = 0 };
            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                cart.ClearCart();
                code = new { Success = true, msg = "Xóa giỏ hành thành công", code = 1, count = 0 };
            }
            Session.Remove("cart");
            return Json(code);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel model)
        {
            var code = new { Success = false, code = -1};
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                if (cart != null && cart.Items.Any())
                {
                    Random x = new Random();
                    Order order = new Order();
                    order.Code = "Đh" + x.Next(0, 9) + x.Next(0, 9) + x.Next(0, 9) + x.Next(0, 100);
                    order.CustomerName = model.CustomerName;
                    order.Email = model.Email;
                    order.Phone = model.Phone;
                    order.Address = model.Address;
                    order.TypeOfPayment = model.TypeOfPayment;
                    order.TotalAmount = cart.GetToTal();
                    order.Quantity = cart.Items.Count;
                    cart.Items.ForEach(y => order.OrderDetails.Add(new OrderDetail 
                        {
                            ProductId = y.ProductId,
                            Price = y.Price,
                            Quantity = y.Quantity
                        }));
                    order.CreateAt = DateTime.Now;
                    order.ModifiderDate = DateTime.Now;
                    order.CreateBy = model.CustomerName;
                    _db.Orders.Add(order);
                    _db.SaveChanges();
                    /*Send mail cho khách hàng*/
                    var strSanPham = "";
                    var thanhTien = Decimal.Zero;
                    var tongTien = Decimal.Zero;
                    foreach(var item in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + item.ProductName + "</td>";
                        strSanPham += "<td>" + item.Quantity + "</td>";
                        strSanPham += "<td>" + WebBanHangOnline.Common.FormatNumber(item.TotalPrice,0) + "</td>";
                        strSanPham += "</tr>";
                        thanhTien += item.Quantity * item.Price;
                    }
                    tongTien = thanhTien;
                    /*mail khách hàng*/
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{Code}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{Product}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{Name}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Address}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
                    contentCustomer = contentCustomer.Replace("{{TimeOrder}}", order.CreateAt.ToString());
                    contentCustomer = contentCustomer.Replace("{{Price}}", WebBanHangOnline.Common.FormatNumber(thanhTien, 0));
                    contentCustomer = contentCustomer.Replace("{{TotalAmount}}",WebBanHangOnline.Common.FormatNumber(tongTien,0));
                    WebBanHangOnline.Common.SendEmail(ConfigurationManager.AppSettings["Email"], order.Email, "Đơn hàng #" + order.Code, contentCustomer.ToString());


                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{Code}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{Product}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{Name}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Address}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{Email}}", order.Email);
                    contentAdmin = contentAdmin.Replace("{{Price}}", WebBanHangOnline.Common.FormatNumber(thanhTien, 0));
                    contentAdmin = contentAdmin.Replace("{{TotalAmount}}", WebBanHangOnline.Common.FormatNumber(tongTien, 0));
                    WebBanHangOnline.Common.SendEmail(ConfigurationManager.AppSettings["Email"], ConfigurationManager.AppSettings["EmailAdmin"], "Đơn hàng mới #" + order.Code, contentAdmin.ToString());


                
                    cart.ClearCart();
                    Session.Remove("cart");
                    return RedirectToAction("CheckOutSuccess","ShoppingCart");
                }
            }
            return Json(code);
        }
        public ActionResult PartialViewFormUser()
        {
            return PartialView();
        }
    }
}