using UnityEngine;

namespace Rewards
{
    public class InstallView : MonoBehaviour
    {
        [SerializeField]
        private DailyRewardView _dailyRewardView;
        
        private DailyRewardController _dailyRewardController;
        
        [SerializeField]
        private WeeklyRewardView _weeklyRewardView;
        
        private WeeklyRewardController _weeklyRewardController;

        [SerializeField]
        private CurrencyView _currencyView;

        [SerializeField] private Transform _placeForUi;
        
        private void Awake()
        {
            //_dailyRewardController = new DailyRewardController(_placeForUi, _dailyRewardView, _currencyView);
            //_weeklyRewardController = new WeeklyRewardController(_weeklyRewardView);
        }
        private void Start()
        {
            //_dailyRewardController.RefreshView();
            //_weeklyRewardController.RefreshWeeklyView();
        }
    }
}
