using System;
using System.Collections.Generic;

namespace BlazorTrainingBatch0.Database.AppDbContextModels;

public partial class TblWallet
{
    public int WalletId { get; set; }

    public string WalletUserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public decimal Balance { get; set; }
}
