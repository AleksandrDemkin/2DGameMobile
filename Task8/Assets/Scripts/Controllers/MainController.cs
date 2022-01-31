using System;
using System.Collections.Generic;
using Abilities;
using Configs;
using Crime;
using Inventory;
using Items;
using Models;
using Profile;
using Rewards;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MainController : BaseController
    {
        public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
            DailyRewardView dailyRewardView, CurrencyView currencyView,
            FightWindowView fightWindowView, StartFightView startFightView,
            List<ItemConfig> itemConfigs, List<AbilityItemConfig> abilityItemConfigs)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _dailyRewardView = dailyRewardView;
            _currencyView = currencyView;
            _fightWindowView = fightWindowView;
            _startFightView = startFightView;
            
            _itemConfigs = itemConfigs;
            _abilityItemConfigs = abilityItemConfigs;

            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }
        
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private readonly DailyRewardView _dailyRewardView;
        private readonly CurrencyView _currencyView;
        private readonly FightWindowView _fightWindowView;
        private readonly StartFightView _startFightView;
        
        private readonly List<ItemConfig> _itemConfigs;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;
        
        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private DailyRewardController _dailyRewardController;
        private FightWindowController _fightWindowController;
        private StartFightController _startFightController;
        private InventoryController _inventoryController;
        private IAbilitiesController _abilitiesController;
        
        protected override void OnDispose()
        {
            DisposeAllControllers();
            
            _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
            base.OnDispose();
        }

        private void OnChangeGameState(GameState state)
        {
            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    _gameController?.Dispose();
                    _inventoryController?.Dispose();
                    _dailyRewardController?.Dispose();
                    break;
                
                case GameState.Game:
                    _inventoryController = new InventoryController(_placeForUi, _itemConfigs);
                    _inventoryController.ShowInventory(null);
                    _inventoryController.HideInventory();
                    
                    _abilitiesController = new AbilitiesController(_placeForUi, _abilityItemConfigs);
                    _abilitiesController.ShowAbilities();
                    
                    _startFightController = new StartFightController(_placeForUi, _profilePlayer, _startFightView); 
                    
                    _gameController = new GameController(_profilePlayer);
                    
                    _mainMenuController?.Dispose();
                    _fightWindowController?.Dispose();
                    break;
                
                case GameState.DailyReward:
                    _dailyRewardController = new DailyRewardController(_placeForUi, 
                        _profilePlayer, _dailyRewardView, _currencyView);
                    _dailyRewardController.RefreshView();
                    
                    _mainMenuController?.Dispose();
                    break;
                
                case GameState.Fight:
                    _fightWindowController = new FightWindowController(_placeForUi, 
                        _profilePlayer, _fightWindowView);
                    _fightWindowController.RefreshView();
                    
                    _gameController.Dispose();
                    _startFightController?.Dispose();
                    break;

                case GameState.Shad:
                    _inventoryController?.Dispose();
                    break;
                
                case GameState.Quit:
                    DisposeAllControllers();
                    break;
                
                default:
                    DisposeAllControllers();
                    break;
            }
        }

        private void DisposeAllControllers()
        {
            _mainMenuController?.Dispose();
            _inventoryController?.Dispose();
            _gameController?.Dispose();
            _fightWindowController?.Dispose();
            _dailyRewardController?.Dispose();
            _startFightController?.Dispose();
        }
    }
}