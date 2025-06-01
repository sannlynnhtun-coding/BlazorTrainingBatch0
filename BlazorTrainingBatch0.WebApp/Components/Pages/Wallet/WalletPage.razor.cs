using BlazorTrainingBatch0.Domain.Features;
using MudBlazor;

namespace BlazorTrainingBatch0.WebApp.Components.Pages.Wallet
{
    public partial class WalletPage
    {
        public string TextValue { get; set; }

        private int pageCount = 0;

        private WalletRequestModel requestModel = new();
        private List<WalletModel> Wallets { get; set; } = new List<WalletModel>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadWalletsAsync();
            }
        }

        private async Task LoadWalletsAsync(int pageNo = 1, int pageSize = 10)
        {
            try
            {
                var response = await WalletService.GetWalletListAsync(pageNo, pageSize);
                if (response.Response.IsErrorV2)
                {
                    Snackbar.Add(response.Response.RespMessage, Severity.Error);
                    return;
                }
                pageCount = response.PageCount;
                Wallets = response.Wallets;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error while loading wallets");
                Snackbar.Add("Error while loading wallets", Severity.Error);
            }
        }

        private async Task Save()
        {
            try
            {
                // validation
                await WalletService.CreateWalletAsync(requestModel);
                Snackbar.Add("Wallet created successfully", Severity.Success);
                requestModel = new();
            }
            catch (Exception ex)
            {
                //Snackbar.Add(ex.ToString(), Severity.Error);
                Logger.LogError(ex, "Error while creating wallet");
            }
        }

        private void Edit(WalletModel model)
        {
            requestModel = new WalletRequestModel
            {
                Balance = model.Balance,
                FullName = model.FullName,
                MobileNo = model.MobileNo,
                WalletUserName = model.WalletUserName
            };
        }

        private async Task Delete(WalletModel model)
        {
            var result = await CommonUserInterfaceService.ShowMessageAsync($"Are you sure you want to delete wallet {model.WalletUserName}?");
            if (result.Canceled) return;

            // process
            Console.WriteLine($"Deleting wallet {model.WalletUserName}");
            await LoadWalletsAsync(1, 10);
        }

        private async Task PageChanged(int pageNo)
        {
            Console.WriteLine(pageNo);
            await LoadWalletsAsync(pageNo, 10);
        }
    }
}
