using System;
using System.Collections.Generic;
using Items;

namespace Inventory
{
    public interface IInventoryView
    {
        event EventHandler<IItem> Selected;
        event EventHandler<IItem> Deselected;
        
        void Display(IReadOnlyList<IItem> items);
        void Hide();
    }
}