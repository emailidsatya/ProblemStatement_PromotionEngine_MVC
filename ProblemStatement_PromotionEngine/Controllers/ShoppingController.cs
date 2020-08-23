using ProblemStatement_PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProblemStatement_PromotionEngine.Controllers
{
    public class ShoppingController : Controller
    {

        // Product and Cart Model
        ProductModel _objProductModel = new ProductModel();
        CartModel _objCart = new CartModel();


        // GET: Shopping
        public ActionResult Index()
        {
            ViewBag.productList = _objProductModel.getAllProductList();
            return View();

        }

        // Add Item to cart
        public ActionResult AddToCart(string id)
        {

            // get product by id
            var productDetails = _objProductModel.getProductById(Convert.ToInt32(id));


            // check session cart is null then add first item
            // else update only quantity
            if (Session["cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel
                {
                    CartId = cart.Count + 1,
                    ProductId = productDetails.Id,
                    Quantity = 1,
                    OriginalPrice = productDetails.Price,
                    OfferPrice = productDetails.Price
                });
                _objCart.CartList = cart;
                Session["cart"] = cart;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["cart"];
                int index = isExist(productDetails.Id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartModel
                    {
                        CartId = cart.Count + 1,
                        ProductId = productDetails.Id,
                        Quantity = 1,
                        OriginalPrice = productDetails.Price,
                        OfferPrice = productDetails.Price
                    });
                }
                _objCart.CartList = cart;
                Session["cart"] = cart;
            }

            return RedirectToAction("Index");
        }

        // Method to check product exist in cart 
        // return true or false
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ProductId == id)
                    return i;
            return -1;
        }
    }
}