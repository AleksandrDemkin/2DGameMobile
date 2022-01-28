
using UnityEngine;
using UnityEngine.UI;

namespace Profile.ScriptsTest
{
    public class CanvasSwitcherController
    {
        private CanvasSwitcherView _canvasSwitcherView;

        private bool _isActive;

        private Canvas _canvas;

        private Button _button;


        public CanvasSwitcherController(CanvasSwitcherView canvasSwitcherView)
        {
            _canvasSwitcherView = canvasSwitcherView;
        }

        private void HideCanvas()
        {
            _canvasSwitcherView.Canvas1.gameObject.SetActive(false);
        }

        private void ButtonClick()
        {
            
        }

    }
}