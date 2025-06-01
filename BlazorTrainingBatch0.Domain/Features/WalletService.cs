using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTrainingBatch0.Domain.Features
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepo _walletRepo;

        public WalletService(IWalletRepo walletRepo)
        {
            _walletRepo=walletRepo;
        }

        public async Task<WalletListResponseMdoel> GetWalletListAsync(int pageNo = 1, int pageSize = 10)
        {
            // validation

            var lst = await _walletRepo.GetWalletListAsync(pageNo, pageSize);
            return lst;
        }

        public async Task<WalletResponseMdoel> CreateWalletAsync(WalletRequestModel requestModel)
        {
            // validation
            // check if wallet exists
            // create wallet
            // save changes
            // await _walletRepo.CreateWalletAsync(model);

            var model = await _walletRepo.CreateWalletAsync(requestModel);
            return model;
        }
    }
}
