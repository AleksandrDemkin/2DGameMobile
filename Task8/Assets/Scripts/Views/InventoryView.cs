using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;
        
        private IReadOnlyList<IItem> _itemInfoCollection;

        public void Display(IReadOnlyList<IItem> itemInfoCollection)
        { 
            _itemInfoCollection = itemInfoCollection;
            foreach (var item in itemInfoCollection)
            {
                Debug.Log($"Id item: {item.Id}, title: {item.Info.Title}");
            }
        }

        public void Hide()
        {
            Debug.Log($"Close Inventory");
        }

        protected virtual void OnSelected(IItem item)
        {
            Selected?.Invoke(this, item);
        }
        
        protected virtual void OnDeselected(IItem item)
        {
            Deselected?.Invoke(this, item);
        }

    }
}