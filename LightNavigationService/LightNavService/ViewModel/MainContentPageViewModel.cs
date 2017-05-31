using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LightNavService.Infrastructure.Interfaces;
using LightNavService.Pages;

namespace LightNavService.ViewModel
{
    public class MainContentPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ICommand NavigateToCarouselPage { get; set; }

        public MainContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToCarouselPage = new RelayCommand(async () => await Navigate(), () => true);
        }

        private async Task Navigate()
        {
            await _navigationService.NavigateTo(App.AppPagesCollection[AppPages.MainCarouselPage]);
        }
    }
}
