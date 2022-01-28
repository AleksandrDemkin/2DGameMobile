using AnalyticsTools;
using Models.Tools;
using Profile;

namespace Models
{
    public class ProfilePlayer
    {
        public ProfilePlayer(float speedCar, AdsTools adsTools)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new CarModel(speedCar);
            AnalyticsTools = new AnalyticsTools.AnalyticsTools();
            AdsShower = adsTools;
        }

        public SubscriptionProperty<GameState> CurrentState { get; }

        public CarModel CurrentCar { get; }

        public IAnalyticsTools AnalyticsTools { get; }
        
        public IAdsShower AdsShower { get; }
    }
}