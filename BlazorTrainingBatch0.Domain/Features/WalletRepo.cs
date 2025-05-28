using BlazorTrainingBatch0.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTrainingBatch0.Domain.Features
{
    public class WalletRepo : IWalletRepo
    {
        private readonly AppDbContext _db;

        public WalletRepo(AppDbContext db)
        {
            _db=db;
        }

        public async Task<WalletListResponseMdoel> GetWalletListAsync(int pageNo = 1, int pageSize = 10)
        {
            var lst = await _db.TblWallets
                .Skip((pageNo - 1) * pageSize)
                .Take(10)
                .ToListAsync();
            var count = await _db.TblWallets.CountAsync(); // 121 / 10 = 13, 12

            var pageCount = count / pageSize; // 12
            if (count % pageSize > 0)
            {
                pageCount++;
            }

            var lstWallet = lst.Select(x => new WalletModel
            {
                Balance = x.Balance,
                FullName = x.FullName,
                MobileNo = x.MobileNo,
                WalletId = x.WalletId,
                WalletUserName = x.WalletUserName
            }).ToList();
            var model = new WalletListResponseMdoel
            {
                Wallets = lstWallet,
                PageCount = pageCount,
                PageNo = pageNo,
                PageSize = pageSize
            };
            return model;
        }
    }

    // walletrequestmodel
    // walletresponsemodel
    // walletmodel

    public class WalletRequestModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class WalletListResponseMdoel
    {
        public List<WalletModel> Wallets { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class WalletResponseMdoel
    {
        public WalletModel Wallet { get; set; }
    }

    public class WalletModel
    {
        public int WalletId { get; set; }

        public string WalletUserName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string MobileNo { get; set; } = null!;

        public decimal Balance { get; set; }
    }
}
