
using UnityEngine;
using UnityEngine.UIElements;

namespace Profile.ScriptsTest
{
    public class CanvasSwitcherView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas1;
        [SerializeField] private Canvas _canvas2;

        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;

        public Canvas Canvas1 => _canvas1;
        public Canvas Canvas2 => _canvas2;

        public Button Button1 => _button1;
        public Button Button2 => _button2;
        
        

    }
}