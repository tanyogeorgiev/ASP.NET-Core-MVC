namespace CarDealer.Web.Infrastructures.Extensions
{
    using System.Globalization;

    public static class StringExtensions
    {

        public static string ToPrice(this decimal priceText)
        {
            return priceText.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
        }

        public static string ToPercent(this double number)
        {
            return $"{(number*100).ToString("F2")}%";
        }

    }
}
