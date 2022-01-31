using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    public class WeeklyRewardController
    {
        private WeeklyRewardView _weeklyRewardView;
        
        private List<ContainerSlotRewardView> _slots;
        
        private bool _isGetReward;
        
        public WeeklyRewardController(WeeklyRewardView weeklyRewardView)
        {
            _weeklyRewardView = weeklyRewardView;
        }
        
        public void RefreshWeeklyView()
        {
            InitSlots();
            _weeklyRewardView.StartCoroutine(RewardsWeekStateUpdater());
            RefreshUi();
            SubscribeButtons();
        }

        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();
            
            for (var i = 0; i < _weeklyRewardView.Rewards.Count; i++)
            {
                var instanceSlot = Object.Instantiate(_weeklyRewardView.ContainerSlotRewardView,
                    _weeklyRewardView.MountRootSlotsReward, false);
                
                _slots.Add(instanceSlot);
            }
        }
        
        private IEnumerator RewardsWeekStateUpdater()
        {
            while (true)
            {
                RefreshRewardsState();
                yield return new WaitForSeconds(1);
            }
        }

        private void RefreshRewardsState()
        {
            _isGetReward = true;

            if (_weeklyRewardView.TimeGetReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _weeklyRewardView.TimeGetReward.Value;
                if (timeSpan.Seconds > _weeklyRewardView.TimeDeadline)
                {
                    _weeklyRewardView.TimeGetReward = null;
                    _weeklyRewardView.CurrentSlotInActive = 0;
                }
                else if (timeSpan.Seconds < _weeklyRewardView.TimeCooldown)
                {
                    _isGetReward = false;
                }
            }
            
            RefreshUi();
        }

        private void RefreshUi()
        {
            _weeklyRewardView.GetRewardButton.interactable = _isGetReward;
            
            if (_isGetReward)
            {
                _weeklyRewardView.TimerNewWeekReward.text = "You have got the reward";
            }
            else
            {
                if (_weeklyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime =
                        _weeklyRewardView.TimeGetReward.Value.AddSeconds(_weeklyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                        $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                    _weeklyRewardView.TimerNewWeekReward.text = $"Time to get the next reward: {timeGetReward}";
                    _weeklyRewardView.TimerNewWeekRewardSlider.wholeNumbers = true;
                    _weeklyRewardView.TimerNewWeekRewardSlider.maxValue = _weeklyRewardView.TimeCooldown;
                    _weeklyRewardView.TimerNewWeekRewardSlider.value = (int)(currentClaimCooldown).TotalSeconds;
                }
            }
            for (var i = 0; i < _slots.Count; i++)
                _slots[i].SetData(_weeklyRewardView.Rewards[i],i + 1, i == _weeklyRewardView.CurrentSlotInActive);
        }
        
        private void SubscribeButtons()
        {
            _weeklyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _weeklyRewardView.ResetButton.onClick.AddListener(ResetTimer);
        }
        
        private void ClaimReward()
        {
            if (!_isGetReward)
                return;
            
            var reward = _weeklyRewardView.Rewards[_weeklyRewardView.CurrentSlotInActive];

            switch (reward.RewardType)
            {
                case RewardType.Coin:
                    CurrencyView.Instance.AddCoin(reward.CountCurrency);
                    break;
                case RewardType.Planet:
                    CurrencyView.Instance.AddPlanet(reward.CountCurrency);
                    break;
                case RewardType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    CurrencyView.Instance.AddDiamond(reward.CountCurrency);
                    break;
            }
            
            _weeklyRewardView.TimeGetReward = DateTime.UtcNow;
            _weeklyRewardView.CurrentSlotInActive = (_weeklyRewardView.CurrentSlotInActive + 1) %
                                                   _weeklyRewardView.Rewards.Count;
            
            RefreshRewardsState();
        }
        
        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}