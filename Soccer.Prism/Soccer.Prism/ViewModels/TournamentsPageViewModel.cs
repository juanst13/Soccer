﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Soccer.Common.Models;
using Soccer.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Soccer.Prism.ViewModels
{

    public class TournamentsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private List<TournamentItemViewModel> _tournaments;

        public TournamentsPageViewModel(INavigationService navigationService, IApiService apiService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Soccer";
            LoadTournamentsAsync();
        }

        public List<TournamentItemViewModel> Tournaments 
        {
            get => _tournaments;
            set => SetProperty(ref _tournaments, value);
        }

        private async void LoadTournamentsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TournamentResponse>(
                url,
                "/api",
                "/Tournaments");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var tournaments = (List<TournamentResponse>)response.Result;
            Tournaments = tournaments.Select(t => new TournamentItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                Groups = t.Groups,
                Id = t.Id,
                IsActive = t.IsActive,
                LogoPath = t.LogoPath,
                Name = t.Name,
                StartDate = t.StartDate
            }).ToList();
        }

    }
}
