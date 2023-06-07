using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models.Payment;
using WebBanHangOnline;
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
        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = _db.Orders.FirstOrDefault(x => x.Code == orderCode);
                        if(itemOrder != null)
                        {
                            itemOrder.Status = 2;
                            _db.Orders.Attach(itemOrder);
                            _db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();

                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        ViewBag.ThanhToanThanhCong = "Số tiền đã giao dịch (VNĐ) là " + vnp_Amount.ToString();

                        /*                        log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                        */
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
/*                        log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
*/                    }
                    /*displayTmnCode.InnerText = "Mã Website (Terminal ID):" + TerminalID;
                    displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    displayAmount.InnerText = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;*/
                }
                else
                {
                    /*log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                    displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý";*/
                }
            }
            /*            var a = urlPayment(0, "Đh35864");
            */
            return View();
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
            var code = new { Success = false, code = -1 , Url = ""};
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
                    order.Status = 1; //1 chưa thanh toán 2 đã thanh toán 3 đã hủy
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
                    code = new { Success = true, code = model.TypeOfPayment, Url = ""};
                    if (model.TypeOfPayment == 2)
                    {
                        var url = urlPayment(model.TypeOfPaymentVNPay, order.Code);
                        code = new { Success = true, code = model.TypeOfPayment, Url = url};
                    }
                    /*return RedirectToAction("CheckOutSuccess","ShoppingCart");*/
                }
            }
            return Json(code);
        }
        public string urlPayment(int typeOfPaymentVnPay,string orderCode)
        {
            var urlPayment = "";
            var order = _db.Orders.FirstOrDefault(x => x.Code == orderCode);

            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key
            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var totalPrice = (long)order.TotalAmount;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (totalPrice * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (typeOfPaymentVnPay == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (typeOfPaymentVnPay == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (typeOfPaymentVnPay == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }
            vnpay.AddRequestData("vnp_CreateDate", order.CreateAt.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());            
            vnpay.AddRequestData("vnp_Locale", "vn");         
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            /*            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            */
            return urlPayment;
        }
        public ActionResult PartialViewFormUser()
        {
            return PartialView();
        }
    }
}