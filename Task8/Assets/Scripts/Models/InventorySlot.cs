using System;
using Items;

namespace Inventory
{
    public class InventorySlot : IInventorySlot
    {
        public IItem Item { get; private set; }
    }
}