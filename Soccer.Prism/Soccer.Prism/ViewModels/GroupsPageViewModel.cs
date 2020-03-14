﻿using Prism.Navigation;
using Soccer.Common.Helpers;
using Soccer.Common.Models;
using System.Collections.Generic;

namespace Soccer.Prism.ViewModels
{
    public class GroupsPageViewModel : ViewModelBase
    {
        private readonly ITransformHelper _transformHelper;
        private TournamentResponse _tournament;
        private List<Group> _groups;

        public GroupsPageViewModel(INavigationService navigationService, 
            ITransformHelper transformHelper) 
            : base(navigationService)
        {
            Title = "Groups";
            _transformHelper = transformHelper;
        }

        ///       public TournamentResponse Tournament
        ///       {
        ///           get => _tournament;
        ///          set => SetProperty(ref _tournament, value);
        ///        }

        public List<Group> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

        ///            if (parameters.ContainsKey("tournament"))
        ///            {
        ///                Tournament = parameters.GetValue<TournamentResponse>("tournament");
        ///                Title = Tournament.Name;
        ///            }

            _tournament = parameters.GetValue<TournamentResponse>("tournament");
            Title = _tournament.Name;
            Groups = _transformHelper.ToGroups(_tournament.Groups);
        }
    }
}
