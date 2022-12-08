using CommunityToolkit.Mvvm.ComponentModel;
using Homiev2.Mobile.Enum;
using Homiev2.Mobile.Services;
using Homiev2.Shared.Dto;
using Homiev2.Shared.Enums;
using Homiev2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homiev2.Mobile.ViewModels
{
    public partial class EditChorePageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService;
        private readonly BaseChoreDto _chore;
        private readonly Task _initTask;

        [ObservableProperty]
        private FrequencyType _choreType;
        [ObservableProperty]
        private SimpleChoreDto _simpleChore;
        [ObservableProperty]
        private AdvancedChoreDto _advancedChore;

        public EditChorePageViewModel(ApiService apiService, BaseChoreDto chore = null) 
        {
            _apiService = apiService;
            _chore = chore;
            _initTask = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (_chore != null)
            {
                _choreType = _chore.FrequencyTypeId;

                switch (_choreType)
                {
                    case FrequencyType.Simple:
                        _simpleChore = await _apiService.ApiRequestAsync<SimpleChoreDto>(ApiRequestType.GET,"Chore/Chore");
                        break;
                    case FrequencyType.Advanced:
                        _advancedChore = await _apiService.ApiRequestAsync<AdvancedChoreDto>(ApiRequestType.GET, "Chore/Chore");
                        break;
                }
            }
        }
    }
}
