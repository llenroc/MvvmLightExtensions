using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LightNavService.Infrastructure.Interfaces;

namespace LightNavService.ViewModel
{
    public class NavigationPageSecondContentPageViewModel: ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ICommand GoBack { get; set; }

        public NavigationPageSecondContentPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoBack = new RelayCommand(async () => await Back(), () => true);
		}

		private async Task Back()
		{
			await _navigationService.GoBack();
		}
    }
}
