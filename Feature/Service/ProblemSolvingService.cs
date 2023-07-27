using SalesTaxes.Problem.Feature.Helpers;
using SalesTaxes.Problem.Feature.Interfaces;
using SalesTaxes.Problem.Feature.Models;

namespace SalesTaxes.Problem.Feature.Service
{
    public class ProblemSolvingService : IProblemSolvingService
    {
        public Product getProductFromString(string? description)
        {
            if (string.IsNullOrEmpty(description)) throw new Exception(Const.messageBadFormat);
            description = description.Trim();
            int quantity = getQuantity(description);
            decimal price = getPrice(description);
            string item = getItemName(description);

            return new Product
            {
                Quantity = quantity,
                Price = price,
                Description = item.Trim()
            };
        }

        private int getQuantity(string description)
        {
            int indexTilQuantity = description.IndexOf(Const.separator);
            if (indexTilQuantity == -1) throw new Exception(Const.messageBadFormat);

            string quantity = description.Substring(0, indexTilQuantity);
            return int.Parse(quantity);

        }

        private decimal getPrice(string description)
        {
            int indexBeforePrice = description.ToUpper().LastIndexOf(Const.separatorAt);
            if (indexBeforePrice == -1) throw new Exception(Const.messageBadFormat);

            indexBeforePrice += Const.separatorAt.Length;
            string price = description.Substring(indexBeforePrice, description.Length - indexBeforePrice);
            return decimal.Parse(price);
        }

        public string getItemName(string description)
        {
            int indexTilQuantity = description.IndexOf(Const.separator);
            int indexBeforePrice = description.ToUpper().LastIndexOf(Const.separatorAt);
            if (indexTilQuantity == -1 || indexBeforePrice == -1) throw new Exception(Const.messageBadFormat);

            indexBeforePrice += Const.separatorAt.Length;
            string item = description.Substring(indexTilQuantity, indexBeforePrice - Const.separatorAt.Length - 1);
            return item.Trim();

        }
    }
}
