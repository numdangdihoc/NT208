using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using Bookstore.Models;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;


namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        BookStoresEntities db = new BookStoresEntities();
        public ActionResult Index()
        {// Lấy danh sách sách từ cơ sở dữ liệu
            List<Book> books = db.Books.ToList();

            // Truyền danh sách sách tới View
            return View(books);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user, string confirm_password)
        {
            if (ModelState.IsValid)
            {
                if (user.user_password == confirm_password)
                {
                    // Mật khẩu khớp, thêm người dùng vào database
                    db.Users.Add(user);
                    db.SaveChanges();
                    // Chuyển hướng đến trang đăng nhập
                    return RedirectToAction("SignIn", "Home");
                }
            }
            TempData["error"] = "Nhập lại mật khẩu không chính xác";
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string user, string password)
        {
            //check db 
            var account = db.Users.SingleOrDefault(m => m.user_name.ToLower() == user.ToLower() && m.user_password == password);
            //check input
            if (account != null)
            {
                Session["user"] = account;
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
        }
        public ActionResult LogOut(string user, string password)
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        public ActionResult AddToCart(int SanPhamID)
        {
            
            Book sp = db.Books.Find(SanPhamID); // Find the product by SanPhamID in the database

            if (sp != null)
            {
               
                // Create a new CartItem
                Cart newItem = new Cart()
                {
                    book_id = sp.book_id,
                    cart_name_book = sp.book_name,
                    cart_image = sp.book_image,
                    cart_price = sp.book_price,
                    cart_quantity = 1
                    
                };
                db.Carts.Add(newItem); // Add the CartItem to the cart
                db.SaveChanges();
                TempData["SuccessMessage"] = "Thêm thành công vào giỏ hàng";
            }
            else
            {
                TempData["ErrorMessage"] = "Product not found!";
            }

            // Redirect back to the previous page or a specific page
            return Redirect(Request.UrlReferrer?.ToString() ?? "/"); // Redirect to the previous page or homepage if the referrer is null
        }

        public ActionResult DeleteCart(int? SanPhamID)
        {
            Cart cart = db.Carts.Find(SanPhamID);
            if (cart != null)
            {
                db.Carts.Remove(cart);
                db.SaveChanges();
            }
            else
            {
                // Xử lý khi Session["giohang"] là null
                TempData["ErrorMessage"] = "Session giohang is null";
            }

            return RedirectToAction("Cart");
        }


            public ActionResult Cart()
        {
            // Truy vấn để lấy tất cả các mục trong giỏ hàng
            List<Cart> allCartItems = db.Carts.ToList();

            // Chuyển dữ liệu giỏ hàng đến view để hiển thị
            return View(allCartItems);
        }


        public class BookDetailViewModel
        {
            public Book BookDetail { get; set; }
            public List<Book> BookList { get; set; }
        }

        public ActionResult DetailBook(int id)
        {
            var sach = db.Books.Find(id);
            List<Book> books = db.Books.ToList();

            // Tạo đối tượng ViewModel và gán dữ liệu
            var viewModel = new BookDetailViewModel
            {
                BookDetail = sach,
                BookList = books
            };

            return View(viewModel);
        }
        public ActionResult Order() 
        {
            List<Cart> cartItems = db.Carts.ToList();
            return View(cartItems);
        }
       
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult SearchByName(string name, int? page)
        {
            ViewBag.search = name;

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            var searchResults = db.Books
                .Where(p => p.book_name.Contains(name))
                .OrderByDescending(x => x.book_name)
                .ToPagedList(pageNumber, pageSize);

            if (searchResults.Count == 0)
            {
                ViewBag.Message = "Không tìm thấy kết quả nào cho \"" + name + "\"";
            }

            return PartialView("SearchByName", searchResults);
        }

    }
}