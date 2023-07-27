namespace SalesTaxes.Problem.Feature.Interfaces
{
    public interface IStoreService
    {
        public bool ReadMedical();
        public bool ReadImported();
        public bool ReadBasket();
        public bool WriteResult();
        public int GetTotalProductsInBasket();
        public int GetTotalProductsExempts();
        public int GetTotalProductsImported();
    }
}
