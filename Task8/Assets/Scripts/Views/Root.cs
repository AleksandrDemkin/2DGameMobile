using System.Linq;
using AnalyticsTools;
using Configs;
using Controllers;
using Crime;
using Models;
using Profile;
using Rewards;
using UnityEngine;

namespace Views
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Transform _placeForUi;
        [SerializeField] private AdsTools _adsTools;
        [SerializeField] private ItemConfig[] _itemConfig;
        [SerializeField] private AbilityItemConfig[] _abilityItemConfigs;
        
        [SerializeField] private DailyRewardView _dailyRewardView;
        [SerializeField] private CurrencyView _currencyView;
        [SerializeField] private FightWindowView _fightWindowView;
        [SerializeField] private StartFightView _startFightView;
        
        private MainController _mainController;
        
        private void Awake()
        {
            var profilePlayer = new ProfilePlayer(15f, _adsTools);
            profilePlayer.CurrentState.Value = GameState.Start;
            _mainController = new MainController(_placeForUi, profilePlayer, 
                _dailyRewardView, _currencyView, _fightWindowView,_startFightView,
                _itemConfig.ToList(), _abilityItemConfigs.ToList());
        }

        protected void OnDestroy()
        {
            _mainController?.Dispose();
        }
    }
}