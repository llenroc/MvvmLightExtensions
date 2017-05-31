using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LightNavService.Infrastructure.Interfaces;
using LightNavService.Pages;

namespace LightNavService.ViewModel
{
    public class NavigationPageFirstContentPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
		public ICommand ShowDetails { get; set; }
		public ICommand GoBack { get; set; }

		public NavigationPageFirstContentPageViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			ShowDetails = new RelayCommand(async () => await ShowDetailsPage(), () => true);
			GoBack = new RelayCommand(async () => await Back(), () => true);  
		}

		private async Task Back()
		{
			await _navigationService.GoBack();
		}

		private async Task ShowDetailsPage()
		{
            await _navigationService.NavigateTo(App.AppPagesCollection[AppPages.NavigationPageSecondContentPage]);
		}
    }
}
