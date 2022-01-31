using System;
using System.Collections.Generic;
using Items;
using Views;

namespace Abilities
{
    public interface  IAbilityCollectionView : IView
    {
        event Action<IItem> UseRequested;

        void Display(IReadOnlyList<IItem> abilityItem);
    }
}