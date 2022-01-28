using System;
using System.Collections.Generic;
using Configs;
using Controllers;
using Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Inventory
{
    public class InventoryController : BaseController, IInventoryController
    {
        private readonly IInventoryModel _inventoryModel;
        private readonly IInventoryView _inventoryView;
        private readonly IItemsRepository _itemsRepository;
        private readonly ResourcePath _viewItemsPanelPath = new ResourcePath {PathResource = "Prefabs/ItemsPanel"};
        private IInventoryController _inventoryControllerImplementation;
        
        public Action HideAction { get; }

        public InventoryController(Transform placeForUi, List<ItemConfig> itemConfigs)
        {
            _inventoryModel = new InventoryModel();
            _itemsRepository = new ItemsRepository(itemConfigs);
            _inventoryView = LoadInventoryView(placeForUi);
            
            SubscribeView();
        }

        private InventoryView LoadInventoryView(Transform placeForUi)
        {
            var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewItemsPanelPath), 
                placeForUi, false);
            return objView.GetComponent<InventoryView>();
        }

        public void ShowInventory(Action callback)
        {
            foreach (var item in _itemsRepository.Items.Values)
                _inventoryModel.EquipItem(item);

            var equippedItem = _inventoryModel.GetEquippedItems();
            _inventoryView.Display(equippedItem);
        }

        public void HideInventory()
        {
            _inventoryView.Hide();
            HideAction?.Invoke();
        }
        
        private void SubscribeView()
        {
            _inventoryView.Selected += OnItemSelected;
            _inventoryView.Deselected += OnItemDeselected;
        }
        
        protected override void OnDispose()
        {
            _inventoryView.Selected -= OnItemSelected;
            _inventoryView.Deselected -= OnItemDeselected;

            base.OnDispose();
        }

        private void OnItemSelected(object sender, IItem item)
        {
            _inventoryModel.EquipItem(item);
        }

        private void OnItemDeselected(object sender, IItem item)
        {
            _inventoryModel.UnequipItem(item);
        }
    }
}