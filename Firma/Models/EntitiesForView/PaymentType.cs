using System.Collections.Generic;

namespace SystemRestauracji.Models.EntitiesForView
{
    public static class PaymentType
    {
        public static List<string> GetPaymentTypes()
        {
            return new List<string>()
            {
                "Gotówka",
                "Karta kredytowa",
                "Przelew",
                "Płatnośc odroczona"
            };
        }
    }
}
