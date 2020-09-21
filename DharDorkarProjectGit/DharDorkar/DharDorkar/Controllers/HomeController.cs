using DharDorkar.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DharDorkar.DAL;
using DharDorkar.Repository;

namespace DharDorkar.Controllers
{
    public class HomeController : Controller
    {
        dbDharDorkarEntities1 ctx = new dbDharDorkarEntities1();
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Index(string search,int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }
        public ActionResult AllProduct(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 8, page));
        }

        public ActionResult ProductDetails(int productId)
        {


            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));

        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            
           
            return View();
        }
        [HttpPost]
        public ActionResult CheckoutDetails(Tbl_Order tbl, HttpPostedFileBase file)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int n = 0;
            foreach (var item in cart)
            { n++; }
            string productnames;
            string[] productquantities = new string[n];
            string productQua;
            double SUM = 0;
            double PRICE;

            string[] productNames = new string[n]; ;
            int a = 0;
            foreach (var item in cart)
            {

                productnames = item.Product.ProductName;
                productquantities[a] = item.Quantity.ToString();
                PRICE = (double)item.Quantity * (double)item.Product.Price;
                SUM += PRICE;
                productNames[a] = productnames;
                //productnames = String.Join(",", productNames);
                //tbl.ProductQuantities = item.Product.Quantity;

                //tbl.ProductNames = productnames;
                a++;
            }
            productnames = String.Join(",", productNames);
            tbl.ProductNames = productnames;
            productQua = String.Join(",", productquantities);
            tbl.ProductQuantities = productQua;
            tbl.TotalPayment = (decimal)SUM;
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("../ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.NationalId = pic;

            _unitOfWork.GetRepositoryInstance<Tbl_Order>().Add(tbl);
            Session["cart"] = null;
            return RedirectToAction("Index");

        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        public ActionResult AddToCart(int productId)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Index");
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = ctx.Tbl_Product.Find(productId);
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
        public ActionResult About()
        {
            return View();
        }
    }




    //View Details 

    
}