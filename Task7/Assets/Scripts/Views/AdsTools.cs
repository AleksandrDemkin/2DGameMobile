using UnityEngine;
using UnityEngine.Advertisements;

namespace AnalyticsTools
{
    public class AdsTools : MonoBehaviour, IAdsShower
    {
        private const string GameId = "4526200";
        private const string BannerPlacementId = "Banner_Android";
        private const string InterstitialVideoId = "Interstitial_Android";
        
        private void Start()
        {
            Advertisement.Initialize(GameId);
        }

        public void ShowBanner()
        {
            Advertisement.Show(BannerPlacementId);
        }

        public void ShowInterstitialVideo()
        {
            Advertisement.Show(InterstitialVideoId);
        }
    }
}
