## Mvvm Light Extensions

This repository contains extension code for Mvvm Light library with Xamarin Forms.


### LightNavigationService - extended Navigation Service for Xamarin Forms and MVVM Light

![](https://thumbs.gfycat.com/SpotlessScornfulAsp-size_restricted.gif)

LightNavigationService was created to handle navigation between different Xamarin Forms pages asynchronously.
Please note that this is first early version so do not hestitate to fork and enhance.

**INavigationService interface**

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
    
**INavigationService implementation (LightNavigationService)**

    public class LightNavigationService: INavigationService
        {
            private readonly ConcurrentDictionary<string, Type> _pagesByKey = new ConcurrentDictionary<string, Type>();

            public Page RootPage { get; set; }
            public Page CurrentPage { get; set; }

        public void Configure(string pageKey, Type pageType)
        {
            if (_pagesByKey.ContainsKey(pageKey))
            {
              _pagesByKey[pageKey] = pageType;
            }
            else
            {
              _pagesByKey.TryAdd(pageKey, pageType);
            }
        }

        public async Task GoBack()
        {
          if (RootPage is NavigationPage)
          {
            var mainPage = RootPage as NavigationPage;
            await mainPage.CurrentPage.Navigation.PopAsync();
          }

          if (RootPage is CarouselPage)
          {
            await CurrentPage.Navigation.PopModalAsync(true);   
          }

           if (RootPage is ContentPage)
              {
                if(RootPage.Navigation.ModalStack.Count>0)
                  {
                     CurrentPage = RootPage.Navigation.ModalStack.Last();
                     await RootPage.Navigation.PopModalAsync(true);
                    }      
                }
        }

        public void InitializeRootPage(Page page)
        {
          RootPage = page;
          Application.Current.MainPage = page;
        }

        public async Task NavigateTo(string pageKey)
        {
          await NavigateTo(pageKey, null);
        }

        public async Task NavigateTo(string pageKey, object parameter)
        {
          if (_pagesByKey.ContainsKey(pageKey))
          {
            var type = _pagesByKey[pageKey];
            ConstructorInfo constructor;
            object[] parameters;

            if (parameter == null)
            {
              constructor = type.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(c => !c.GetParameters().Any());

              parameters = new object[]
              {
              };
            }
            else
            {
              constructor = type.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(
                  c =>
                  {
                    var p = c.GetParameters();
                    return p.Count() == 1 && p[0].ParameterType == parameter.GetType();
                  });

              parameters = new[]
              {
                  parameter
                };
            }

            if (constructor == null)
            {
              throw new InvalidOperationException(
                "No suitable constructor found for page " + pageKey);
            }

            var page = constructor.Invoke(parameters) as Page;

                    await HandleNavigation(page);
          }
          else
          {
            throw new ArgumentException(
              string.Format(
                "No such page: {0}. Did you forget to call NavigationService.Configure?",
                pageKey), nameof(pageKey));
          }
        }

        private async Task HandleNavigation(Page pageToNavigate)
         {
            if (RootPage is NavigationPage)
            {
              var navigationPage = RootPage as NavigationPage;
              await navigationPage.Navigation.PushAsync(pageToNavigate);
            }

            if (RootPage is CarouselPage)
            {
              var contentPage = RootPage as CarouselPage;
              await contentPage.Navigation.PushModalAsync(pageToNavigate, true);
            }

            if (RootPage is ContentPage)
            {
              var contentPage = RootPage as ContentPage;
                        if(CurrentPage is NavigationPage)
                        await CurrentPage.Navigation.PushAsync(pageToNavigate, true);
                    else
                        await contentPage.Navigation.PushModalAsync(pageToNavigate, true);
            }   
               CurrentPage = pageToNavigate;
           }
        }

Sample application is available in repository (dev branch).
