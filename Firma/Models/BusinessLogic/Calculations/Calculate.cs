namespace SystemRestauracji.Models.BusinessLogic.Calculations
{
    public static class Calculate
    {
        public static decimal? CalculateNetPrice(decimal? grossPrice, decimal? vat)
        {
            if(grossPrice == null)
            {
                return null;
            }
            if (grossPrice == 0)
            {
                return 0;
            }
            if (vat == null || vat == 0)
            {
                return grossPrice;
            }
            return grossPrice / ((100 + vat) / 100);
        }

        public static decimal? CalculateGrossPriceWithQuantity(decimal? netPrice, decimal? vat, int? quantity)
        {
            if (quantity == null || netPrice == null)
            {
                return null;
            }
            if (quantity == 0 || netPrice == 0)
            {
                return 0;
            }

            if (vat == null || vat == 0)
            {
                return netPrice;
            }
            return ((netPrice + (netPrice * (vat / 100))) * quantity);
        }
    }
}
