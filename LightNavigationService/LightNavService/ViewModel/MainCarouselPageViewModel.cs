using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LightNavService.Infrastructure.Interfaces;
using LightNavService.Pages;

namespace LightNavService.ViewModel
{
    public class MainCarouselPageViewModel: ViewModelBase
    {
		private readonly INavigationService _navigationService;
		public ICommand NavigateToNavigationPage { get; set; }
        public ICommand GoBack { get; set; }

        public MainCarouselPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
			NavigateToNavigationPage = new RelayCommand(async () => await Navigate(), () => true);
            GoBack = new RelayCommand(async () => await Back(), () => true);
        }

		private async Task Navigate()
		{
            await _navigationService.NavigateTo(App.AppPagesCollection[AppPages.MainNavigationPage], new NavigationPageFirstContentPage());
		}

		private async Task Back()
		{
            await _navigationService.GoBack();
		}

    }
}
