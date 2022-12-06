using Homiev2.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.ViewModels
{
    public class ChoresPageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;

        public ChoresPageViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }
    }
}
