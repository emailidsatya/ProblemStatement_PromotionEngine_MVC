using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProblemStatement_PromotionEngine.Models
{
    public class SetOfferPlan
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string ComboProductId { get; set; }
        public string OnProduct { get; set; }
        public decimal Price { get; set; }
        public int ItemCount { get; set; }
        public List<SetOfferPlan> OfferPlan { get; set; }

        // Get offer List
        public List<SetOfferPlan> getOfferPlan()
        {
            List<SetOfferPlan> offerList = new List<SetOfferPlan>()
            {
                new SetOfferPlan{PlanId = 1,PlanName = "3 Of A's",ComboProductId = "0",OnProduct ="1",Price = 130,ItemCount =3},
                new SetOfferPlan{PlanId = 1,PlanName = "2 Of B's",ComboProductId = "0",OnProduct ="2",Price = 45,ItemCount =2},
                new SetOfferPlan{PlanId = 1,PlanName = "C & D",ComboProductId = "3,4",OnProduct ="0",Price = 30,ItemCount =1},
            };

            return offerList;

        }
    }
}