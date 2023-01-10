using Homiev2.Mobile.Services;
using Homiev2.Mobile.ViewModels;
using Homiev2.Mobile.Views;

namespace Homiev2.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont(filename: "materialdesignicons-webfont.ttf", alias: "MaterialDesignIcons");
                });

            //Services
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddHttpClient("HttpClient", opt =>
            {
//#if DEBUG
//                var uri = new Uri("http://10.0.2.2:5074");
//#else
                var uri = new Uri("https://homiev2.azurewebsites.net:443");
//#endif

                opt.BaseAddress = uri;
                opt.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //ViewModels
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<RegisterPageViewModel>();
            builder.Services.AddSingleton<HouseholdPageViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ChoresPageViewModel>();
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddTransient<AddChorePageViewModel>();
            builder.Services.AddTransient<EditChorePageViewModel>();

            //Views
            builder.Services.AddSingleton<LoginPageView>();
            builder.Services.AddSingleton<RegisterPageView>();
            builder.Services.AddSingleton<HouseholdPageView>();
            builder.Services.AddSingleton<MainPageView>();
            builder.Services.AddSingleton<ChoresPageView>();
            builder.Services.AddSingleton<LoadingPageView>();
            builder.Services.AddTransient<AddChorePageView>();
            builder.Services.AddTransient<EditChorePageView>();


            return builder.Build();

            //TODO remove android:usesCleartextTraffic="true" & android:debuggable="true"  from AndroidManifest.xml 

        }
    }
}
