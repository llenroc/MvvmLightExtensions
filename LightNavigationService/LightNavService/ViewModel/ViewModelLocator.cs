/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:LightNavService"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace LightNavService.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainContentPageViewModel>();
            SimpleIoc.Default.Register<MainCarouselPageViewModel>();
            SimpleIoc.Default.Register<NavigationPageFirstContentPageViewModel>();
			SimpleIoc.Default.Register<NavigationPageSecondContentPageViewModel>();
        }

        public MainContentPageViewModel MainContentPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainContentPageViewModel>();
            }
        }

		public MainCarouselPageViewModel MainCarouselPageViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<MainCarouselPageViewModel>();
			}
		}

		public NavigationPageFirstContentPageViewModel NavigationPageFirstContentPageViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<NavigationPageFirstContentPageViewModel>();
			}
		}

		public NavigationPageSecondContentPageViewModel NavigationPageSecondContentPageViewModel
		{
			get
			{
				return ServiceLocator.Current.GetInstance<NavigationPageSecondContentPageViewModel>();
			}
		}
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}