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
                PageSize = pageSize,
                Response = new ResponseModel("MS#0001", "Success")
            };
            return model;
        }

        public async Task<WalletResponseMdoel> CreateWalletAsync(WalletRequestModel requestModel)
        {
            var wallet = new TblWallet
            {
                WalletUserName = requestModel.WalletUserName,
                FullName = requestModel.FullName,
                MobileNo = requestModel.MobileNo,
                Balance = requestModel.Balance
            };
            await _db.TblWallets.AddAsync(wallet);
            await _db.SaveChangesAsync();
            var model = new WalletResponseMdoel
            {
                Wallet = new WalletModel
                {
                    WalletId = wallet.WalletId,
                    WalletUserName = wallet.WalletUserName,
                    FullName = wallet.FullName,
                    MobileNo = wallet.MobileNo,
                    Balance = wallet.Balance
                },
                Response = new ResponseModel("MS#0002", "Success")
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

        public string WalletUserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public decimal Balance { get; set; }
    }

    public class WalletListResponseMdoel : BaseResponseModel
    {
        public List<WalletModel> Wallets { get; set; }
        public int PageCount { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class WalletResponseMdoel : BaseResponseModel
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

    public class BaseResponseModel
    {
        public ResponseModel Response { get; set; }
    }

    public class ResponseModel
    {
        public ResponseModel(EnumRespType enumRespType, string message)
        {

        }

        public ResponseModel(string respCode, string? respDesp = null)
        {
            RespCode = respCode;
            //RespType = RespCode switch
            //{
            //    "MS0001" => EnumRespType.Success,
            //    "ME0001" => EnumRespType.Error,
            //    "MW0001" => EnumRespType.Warning,
            //    "MI0001" => EnumRespType.Info,
            //    _ => EnumRespType.None
            //};

            string[] respType = respCode.Split("#", StringSplitOptions.RemoveEmptyEntries);
            RespType = respType[0] switch
            {
                "MS" => EnumRespType.Success,
                "ME" => EnumRespType.Error,
                "MW" => EnumRespType.Warning,
                "MI" => EnumRespType.Info,
                _ => EnumRespType.None
            };
        }

        public string RespCode { get; set; } // MS#0001,ME#0001
        public string RespMessage { get; set; } = null!;
        //public bool IsSuccess { get; set; } = false;
        public bool IsSuccess => RespType == EnumRespType.Success;
        public EnumRespType RespType { get; set; }
        //public bool IsError { get { return !IsError; } }

        public bool IsErrorV2 => !IsSuccess;
        public bool IsError()
        {
            return !IsSuccess;
        }
    }

    public enum EnumRespType
    {
        None = 0,
        Success = 1,
        Error = 2,
        Warning = 3,
        Info = 4
    }
}
