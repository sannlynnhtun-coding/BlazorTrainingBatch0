
namespace BlazorTrainingBatch0.Domain.Features
{
    public interface IWalletService
    {
        Task<WalletListResponseMdoel> GetWalletListAsync(int pageNo = 1, int pageSize = 10);
        Task<WalletResponseMdoel> CreateWalletAsync(WalletRequestModel requestModel);
    }
}