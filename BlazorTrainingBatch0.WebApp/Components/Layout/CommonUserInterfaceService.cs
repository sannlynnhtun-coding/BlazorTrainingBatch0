using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorTrainingBatch0.WebApp.Components.Layout
{
    public class CommonUserInterfaceService
    {
        //[Inject]
        //public IDialogService DialogService { get; set; }

        private readonly IDialogService DialogService;

        public CommonUserInterfaceService(IDialogService dialogService)
        {
            DialogService=dialogService;
        }

        public async Task<DialogResult> ShowMessageAsync(string message)
        {
            var parameters = new DialogParameters<Dialog>
            {
                { x => x.Body, message },
                { x => x.Color, Color.Success }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            var result = await DialogService.ShowAsync<Dialog>("Delete", parameters, options);
            var dialogResult = await result!.Result!;
            return dialogResult!;
        }
    }
}
