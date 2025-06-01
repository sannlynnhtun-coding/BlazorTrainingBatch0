
namespace BlazorTrainingBatch0.Domain.Features
{
    public interface IWalletRepo
    {
        Task<WalletResponseMdoel> CreateWalletAsync(WalletRequestModel requestModel);
        Task<WalletListResponseMdoel> GetWalletListAsync(int pageNo = 1, int pageSize = 10);
    }
}