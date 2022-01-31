using System.Collections.Generic;
using Items;
using System;

namespace Inventory
{
    public interface IInventoryModel
    {
        public IReadOnlyList<IItem> GetEquippedItems();
        
        public void EquipItem(IItem item);
        
        public void UnequipItem(IItem item);
    }
}