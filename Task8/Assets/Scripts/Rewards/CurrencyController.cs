using Controllers;
using UnityEngine;

namespace Rewards
{
    public class CurrencyController : BaseController
    {
        private CurrencyView _currencyViewInstance;

        public CurrencyController(Transform placeForUi, CurrencyView currencyView)
        {
            _currencyViewInstance = Object.Instantiate(currencyView, placeForUi);
            AddGameObjects(_currencyViewInstance.gameObject);
        }

        public void CloseWindow()
        {
            Object.Destroy(_currencyViewInstance.gameObject);
        }
    }
}