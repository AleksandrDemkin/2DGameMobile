using System;
using System.Collections.Generic;
using Configs;
using Controllers;
using Inventory;
using Items;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

namespace Abilities
{
    public class AbilitiesController : BaseController, IAbilitiesController
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IAbilityCollectionView _abilityCollectionView;
        private readonly AbilityRepository _abilityRepository;
        private readonly ResourcePath _viewAbilityPanelPath = new ResourcePath {PathResource = "Prefabs/MainAbilityPanel"};
        private IAbilitiesController abilityControllerImplementation;
        private IReadOnlyList<IItem> _abilityItems;
        
        public Action HideAction { get; }

        public AbilitiesController(Transform placeForUi, List<AbilityItemConfig> abilityItemConfigs)
        {
            _inventoryModel = new InventoryModel();
            _abilityRepository = new AbilityRepository(abilityItemConfigs);
            _abilityCollectionView = LoadAbilityView(placeForUi);
            
        }
        
        private IAbilityCollectionView LoadAbilityView(Transform placeForUi)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewAbilityPanelPath), 
                placeForUi, false);
            return objView.GetComponent<AbilityCollectionView>();
        }

        
        public void HideAbilities()
        {
            _abilityCollectionView.Hide();
            HideAction?.Invoke();
        }

        public void ShowAbilities()
        {
            foreach (var item in _abilityItems)
                _inventoryModel.EquipItem(item);

            var equippedItem = _inventoryModel.GetEquippedItems();
            _abilityCollectionView.Display(equippedItem);
        }
        
        /*private void SubscribeView()
        {
            _abilityCollectionView.Selected += OnItemSelected;
            _abilityCollectionView.Deselected += OnItemDeselected;
        }
        
        protected override void OnDispose()
        {
            _abilityCollectionView.Selected -= OnItemSelected;
            _abilityCollectionView.Deselected -= OnItemDeselected;

            base.OnDispose();
        }

        private void OnItemSelected(object sender, IItem item)
        {
            _inventoryModel.EquipItem(item);
        }

        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnequipItem(item);
        }*/
    }
}