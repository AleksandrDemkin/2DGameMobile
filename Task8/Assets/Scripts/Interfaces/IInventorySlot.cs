using Items;
using System;

namespace Inventory
{
    public interface IInventorySlot
    {
        public IItem Item { get; }
        
    }
}