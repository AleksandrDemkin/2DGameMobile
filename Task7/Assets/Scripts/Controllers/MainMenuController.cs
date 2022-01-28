using Models;
using Profile;
using UnityEngine;
using Views;

namespace Controllers
{
    public class MainMenuController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _mainMenuView = LoadView(placeForUi);
            _mainMenuView.Init(StartGame, QuitGame);
        }
    
        private MainMenuView LoadView(Transform placeForUi)
        {
            var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
            AddGameObjects(objectView);
        
            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
            _profilePlayer.AnalyticsTools.SendMessage("Start game", ("Time", Time.realtimeSinceStartup));
            _profilePlayer.AnalyticsTools.SendMessage("Start game");
            _profilePlayer.AnalyticsTools.SendMessage("Start game", ("Time", Time.captureFramerate));
            //_profilePlayer.AdsShower.ShowBanner();
            //_profilePlayer.AdsShower.ShowInterstitialVideo();
        }
        private void QuitGame()
        {
            _profilePlayer.CurrentState.Value = GameState.Quit;
            _profilePlayer.AnalyticsTools.SendMessage("Quit game", ("Time", Time.realtimeSinceStartup));
            _profilePlayer.AnalyticsTools.SendMessage("Quit game");
        }
    }
}