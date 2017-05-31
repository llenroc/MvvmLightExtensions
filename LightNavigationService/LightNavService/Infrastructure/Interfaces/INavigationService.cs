using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightNavService.Infrastructure.Interfaces
{
    public interface INavigationService
	{
		Page RootPage { get; set; }
		Page CurrentPage { get; set; }
       
        void Configure(string pageKey, Type pageType);
		void InitializeRootPage(Page page);
		Task GoBack();
		Task NavigateTo(string pageKey);
		Task NavigateTo(string pageKey, object parameter);

	}
}
