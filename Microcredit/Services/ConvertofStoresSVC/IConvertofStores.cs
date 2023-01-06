using Microcredit.Models;

namespace Microcredit.ClassProject.ConvertofStoresSVC
{
    public interface IConvertofStores
    {

        //Task<List<ConvertofStoresT>> GetAllConvertofStoresAsync();

        Task<ConvertofStoresT> GetConvertofStoresByidAsync(int ConvertofStoresId);

        Task<ResponseObject> CreateConvertofStoresAsync(ConvertofStoresT convertofStoresT);

        Task<bool> UpdateConvertofStoresAsync(int ConvertofStoresId, ConvertofStoresT convertofStoresT);
        Task<bool> DeleteConvertofStoresAsync(int ConvertofStoresId);
        public IEnumerable<ConvertofStoresT> GetAllConvertofStoresAsync(string SPName);
        //public IEnumerable<ConvertofStoresT> GetAllConvertofStores();


    }
}
