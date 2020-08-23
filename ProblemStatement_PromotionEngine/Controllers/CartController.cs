using ProblemStatement_PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProblemStatement_PromotionEngine.Controllers
{
    public class CartController : Controller
    {
        // Model Product and Cart 
        ProductModel objProduct = new ProductModel();
        List<CartModel> cart = new List<CartModel>();


        // GET: Cart
        public ActionResult Index()
        {
            // get cart data ffrom session
            // typecasting to session list
            cart = (List<CartModel>)Session["cart"];

            // if cart is not null
            if (cart != null)
            {
                // check eligible  offers based on product
                Offer checkOffer = new Offer(cart);
                cart = checkOffer.getOfferDiscount();
            }
            ViewBag.carList = cart;
            return View();
        }

        //Remove cart item ffrom cart list
        public ActionResult Remove(string id)
        {
            cart = (List<CartModel>)Session["cart"];

            var isExits = (from u in cart
                           where u.ProductId == Convert.ToInt32(id)
                           select u).FirstOrDefault();

            if (isExits.Quantity > 1)
            {
                foreach (var item in cart.Where(w => w.ProductId == Convert.ToInt32(id)))
                {
                    item.Quantity = isExits.Quantity - 1;
                }
            }
            else
            {
                cart.Remove(isExits);
            }
            // after removing item from list check again offer plan
            Offer checkOffer = new Offer(cart);
            Session["cart"] = cart;

            return RedirectToAction("Index");
        }
    }
}