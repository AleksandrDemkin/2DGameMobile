using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Models;
using Profile;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    public class DailyRewardController : BaseController
    {
        private DailyRewardView _dailyRewardView;
        private List<ContainerSlotRewardView> _slots = new List<ContainerSlotRewardView>();
        private CurrencyController _currencyController;
        private ProfilePlayer _profilePlayer;
        
        private bool _isGetReward;
        
        public DailyRewardController(Transform placeForUi, ProfilePlayer profilePlayer,
            DailyRewardView dailyRewardView, CurrencyView currencyView)
        {
            _profilePlayer = profilePlayer;
            
            _dailyRewardView = Object.Instantiate(dailyRewardView, placeForUi);
            AddGameObjects(_dailyRewardView.gameObject);
            
            _currencyController = new CurrencyController(placeForUi, currencyView);
            AddController(_currencyController);
        }
        
        public void RefreshView()
        {
            InitSlots();
            
            _dailyRewardView.StartCoroutine(RewardsStateUpdater());
            
            RefreshUi();
            SubscribeButtons();
        }

        private void InitSlots()
        {
            for (var i = 0; i < _dailyRewardView.Rewards.Count; i++)
            {
                var instanceSlot = Object.Instantiate(_dailyRewardView.ContainerSlotRewardView,
                    _dailyRewardView.MountRootSlotsReward, false);
                
                _slots.Add(instanceSlot);
            }
        }
        
        private IEnumerator RewardsStateUpdater()
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

            if (_dailyRewardView.TimeGetReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _dailyRewardView.TimeGetReward.Value;
                
                if (timeSpan.Seconds > _dailyRewardView.TimeDeadline)
                {
                    _dailyRewardView.TimeGetReward = null;
                    _dailyRewardView.CurrentSlotInActive = 0;
                }
                
                else if (timeSpan.Seconds < _dailyRewardView.TimeCooldown)
                {
                    _isGetReward = false;
                }
            }
            
            RefreshUi();
        }

        private void RefreshUi()
        {
            _dailyRewardView.GetRewardButton.interactable = _isGetReward;
            
            if (_isGetReward)
            {
                _dailyRewardView.TimerNewReward.text = "You have got the reward";
            }
            else
            {
                if (_dailyRewardView.TimeGetReward != null)
                {
                    var nextClaimTime =
                        _dailyRewardView.TimeGetReward.Value.AddSeconds(_dailyRewardView.TimeCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward = $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:" +
                                        $"{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";
                    _dailyRewardView.TimerNewReward.text = $"Time to get the next reward: {timeGetReward}";
                }
            }
            for (var i = 0; i < _slots.Count; i++)
                _slots[i].SetData(_dailyRewardView.Rewards[i],i + 1, i == _dailyRewardView.CurrentSlotInActive);
        }
        
        private void SubscribeButtons()
        {
            _dailyRewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _dailyRewardView.ResetButton.onClick.AddListener(ResetTimer);
            _dailyRewardView.CloseWindow.onClick.AddListener(CloseWindow);
        }
        
        private void ClaimReward()
        {
            if (!_isGetReward)
                return;
            
            var reward = _dailyRewardView.Rewards[_dailyRewardView.CurrentSlotInActive];

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
            
            _dailyRewardView.TimeGetReward = DateTime.UtcNow;
            _dailyRewardView.CurrentSlotInActive = (_dailyRewardView.CurrentSlotInActive + 1) %
                                                   _dailyRewardView.Rewards.Count;
            
            RefreshRewardsState();
        }
        
        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
            CurrencyView.Instance.AddCoin(0);
            CurrencyView.Instance.AddPlanet(0);
            CurrencyView.Instance.AddWood(0);
            CurrencyView.Instance.AddDiamond(0);
        }

        private void CloseWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
        }

        private new void OnDispose()
        {
            _dailyRewardView.GetRewardButton.onClick.RemoveAllListeners();
            _dailyRewardView.ResetButton.onClick.RemoveAllListeners();
            _dailyRewardView.CloseWindow.onClick.RemoveAllListeners();
            
            base.OnDispose();
        }
    }
}