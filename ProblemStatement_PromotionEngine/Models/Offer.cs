using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProblemStatement_PromotionEngine.Models
{
    public class Offer : SetOfferPlan
    {
        List<CartModel> objCartList;


        // parameterized constructor
        public Offer(List<CartModel> cartList)
        {
            this.objCartList = cartList;
        }

        // Calculate price after discount
        public List<CartModel> getOfferDiscount()
        {
            List<SetOfferPlan> offerList = getOfferPlan();

            //First On Product Wise
            var FirstOffer = offerList.Where(p => p.ComboProductId == "0").ToList();
            foreach (var offer in FirstOffer)
            {
                int ProductId = Convert.ToInt32(offer.OnProduct);
                int Itemcount = offer.ItemCount;

                foreach (var product in objCartList)
                {
                    if (product.ProductId == ProductId && product.Quantity >= Itemcount)
                    {
                        int OffeProctVlaue = product.Quantity / Itemcount;
                        product.OfferPrice = (offer.Price * OffeProctVlaue) + ((product.Quantity % Itemcount) * product.OriginalPrice);
                    }
                }
            }

            // Combo Offer
            var SecondOffer = offerList.Where(p => p.ComboProductId != "0").ToList();


            foreach (var offer in SecondOffer)
            {
                int countOfFirstProduct = 0;
                int countOfSecondProduct = 0;

                foreach (var item in objCartList)
                {
                    if (item.ProductId == Convert.ToInt32(offer.ComboProductId.Split(',')[0]))
                    {
                        countOfFirstProduct += item.Quantity;
                    }
                    if (item.ProductId == Convert.ToInt32(offer.ComboProductId.Split(',')[1]))
                    {
                        countOfSecondProduct += item.Quantity;
                    }
                }

                if (countOfFirstProduct > 0 && countOfSecondProduct > 0)
                {
                    int TotalQuantity = countOfFirstProduct + countOfSecondProduct;
                    int OffeProctVlaue = 0;

                    if (countOfSecondProduct > countOfFirstProduct)
                    {
                        OffeProctVlaue = countOfSecondProduct - countOfFirstProduct;

                        var query = from x in objCartList
                                    where x.ProductId == Convert.ToInt32(offer.ComboProductId.Split(',')[1])
                                    select new { x };
                        foreach (var item in query)
                            item.x.OfferPrice = item.x.Quantity * OffeProctVlaue + ((TotalQuantity - OffeProctVlaue) * offer.Price);
                    }
                    else if (countOfFirstProduct > countOfSecondProduct)
                    {
                        OffeProctVlaue = countOfFirstProduct - countOfSecondProduct;

                        var query = from x in objCartList
                                    where x.ProductId == Convert.ToInt32(offer.ComboProductId.Split(',')[0])
                                    select new { x };
                        foreach (var item in query)
                            item.x.OfferPrice = item.x.Quantity * OffeProctVlaue + ((TotalQuantity - OffeProctVlaue) * offer.Price);
                    }
                }
            }

            return objCartList;
        }

    }
}