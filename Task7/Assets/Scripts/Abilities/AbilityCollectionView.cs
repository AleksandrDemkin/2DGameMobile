using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Abilities
{
    public class AbilityCollectionView : MonoBehaviour, IAbilityCollectionView
    {

        [SerializeField] private Button _buttonShow;
        [SerializeField] private Button _buttonHide;
        [SerializeField] private Canvas _abilityPanel;

        private IReadOnlyList<IItem> _abilityItems;

        public event Action<IItem> UseRequested;

        private void OnUseRequested(IItem item)
        {
            UseRequested?.Invoke(item);
        }

        public void Display(IReadOnlyList<IItem> abilityItems)
        {
            _abilityItems = abilityItems;
        }
        
        public void Init(UnityAction show, UnityAction hide)
        {
            _buttonShow.onClick.AddListener(Show);
            _buttonHide.onClick.AddListener(Hide);
        }
        
        public void Show()
        {
            _abilityPanel.enabled = true;
        }

        public void Hide()
        {
            _abilityPanel.enabled = false;
        }
        
        protected void OnDestroy()
        {
            _buttonShow.onClick.RemoveAllListeners();
            _buttonHide.onClick.RemoveAllListeners();
        }
    }
}
