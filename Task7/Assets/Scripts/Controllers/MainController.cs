using System;
using System.Collections.Generic;
using Abilities;
using Configs;
using Inventory;
using Items;
using Models;
using Profile;
using UnityEngine;

namespace Controllers
{
    public class MainController : BaseController
    {
        public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
            List<ItemConfig> itemConfigs, List<AbilityItemConfig> abilityItemConfigs)
        {
            _profilePlayer = profilePlayer;
            _placeForUi = placeForUi;
            _itemConfigs = itemConfigs;
            _abilityItemConfigs = abilityItemConfigs;

            OnChangeGameState(_profilePlayer.CurrentState.Value);
            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        }

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private InventoryController _inventoryController;
        private IAbilitiesController _abilitiesController;
        
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;
        private readonly List<ItemConfig> _itemConfigs;
        private readonly List<AbilityItemConfig> _abilityItemConfigs;

        protected override void OnDispose()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
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
                    break;
                
                case GameState.Game:
                    _inventoryController = new InventoryController(_placeForUi, _itemConfigs);
                    _inventoryController.ShowInventory(null);
                    _inventoryController.HideInventory();
                    _abilitiesController = new AbilitiesController(_placeForUi, _abilityItemConfigs);
                    _abilitiesController.ShowAbilities();

                    _gameController = new GameController(_profilePlayer);
                    _mainMenuController?.Dispose();
                    break;
                
                case GameState.Shad:
                    _inventoryController?.Dispose();
                    break;
                
                default:
                    _mainMenuController?.Dispose();
                    _inventoryController?.Dispose();
                    _gameController?.Dispose();
                    break;
            }
        }
    }
}