using Xamarin.Forms;
using LightNavService.Infrastructure.Interfaces;
using LightNavService.Infrastructure.Services;
using LightNavService.Pages;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LightNavService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitNavigation();
        }
        public static Dictionary<AppPages, string> AppPagesCollection { get; set; }
		private void InitNavigation()
		{
            AppPagesCollection = new Dictionary<AppPages, string>()
            {
                {
                    AppPages.MainContentPage, "MainContentPage"
                },
				{
                    AppPages.MainCarouselPage, "MainCarouselPage"
				},
				{
                    AppPages.MainNavigationPage, "MainNavigationPage"
				},
				{
                    AppPages.NavigationPageFirstContentPage, "NavigationPageFirstContentPage"
				},
				{
                    AppPages.NavigationPageSecondContentPage, "NavigationPageSecondContentPage"
				}

            };


			INavigationService navigationService;

            navigationService = new LightNavigationService();

            navigationService.Configure(AppPagesCollection[AppPages.MainContentPage], typeof(MainContentPage));
            navigationService.Configure(AppPagesCollection[AppPages.MainCarouselPage], typeof(MainCarouselPage));
            navigationService.Configure(AppPagesCollection[AppPages.MainNavigationPage], typeof(MainNavigationPage));
            navigationService.Configure(AppPagesCollection[AppPages.NavigationPageFirstContentPage], typeof(NavigationPageFirstContentPage));
			navigationService.Configure(AppPagesCollection[AppPages.NavigationPageSecondContentPage], typeof(NavigationPageSecondContentPage));

			SimpleIoc.Default.Register(() => navigationService);

            var firstPage = new MainContentPage();
			navigationService.InitializeRootPage(firstPage);
			MainPage = firstPage;
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
