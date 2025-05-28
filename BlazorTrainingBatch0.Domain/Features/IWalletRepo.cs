
namespace BlazorTrainingBatch0.Domain.Features
{
    public interface IWalletRepo
    {
        Task<WalletListResponseMdoel> GetWalletListAsync(int pageNo = 1, int pageSize = 10);
    }
}