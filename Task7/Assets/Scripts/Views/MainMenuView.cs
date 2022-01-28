using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonQuit;

        public void Init(UnityAction startGame, UnityAction quitGame)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonQuit.onClick.AddListener(quitGame);
        }

        protected void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
        }
    }
}