using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemStatement_PromotionEngine.Controllers;
using ProblemStatement_PromotionEngine.Models;
using System.Collections.Generic;

namespace ProblemStatement_PromotionEngine.Tests
{
    [TestClass]
    public class CartControllerUnitTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            CartController controller = new CartController();
            
            // Act

            //Accepted Value = 280
            decimal expectedTotal = 100;

            List<CartModel> objCartList = new List<CartModel>()
            {
               new CartModel{CartId = 1,ProductId= 1,Quantity =1,OriginalPrice =50,OfferPrice = 50},
               new CartModel{CartId = 2,ProductId= 2,Quantity =1,OriginalPrice =30,OfferPrice =30},
               new CartModel{CartId = 3,ProductId= 3,Quantity =1,OriginalPrice =20,OfferPrice = 20},
               // new CartModel{CartId = 4,ProductId= 4,Quantity =1,OriginalPrice =15,OfferPrice = 15}

            };

            Offer checkOfferPrice = new Offer(objCartList);
            //var result = controller.Index() as ViewResult;
            List<CartModel> obj = checkOfferPrice.getOfferDiscount();

            decimal TotalOfferPrice = 0;
            foreach (var item in obj)
            {
                TotalOfferPrice += item.OfferPrice;
            }
            // Assert
            Assert.AreEqual(TotalOfferPrice, expectedTotal);
        }
    }
}
