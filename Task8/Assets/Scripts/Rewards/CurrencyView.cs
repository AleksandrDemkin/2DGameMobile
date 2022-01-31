using System;
using TMPro;
using UnityEngine;

namespace Rewards
{
    public class CurrencyView : MonoBehaviour
    {
        private const string CoinKey = nameof(CoinKey);
        private const string PlanetKey = nameof(PlanetKey);
        private const string WoodKey = nameof(WoodKey);
        private const string DiamondKey = nameof(DiamondKey);
        
        public static CurrencyView Instance { get; private set; }

        [SerializeField] private TMP_Text _currentCountCoin;
        [SerializeField] private TMP_Text _currentCountPlanet;
        [SerializeField] private TMP_Text _currentCountWood;
        [SerializeField] private TMP_Text _currentCountDiamond;

        private void Awake()
        {
            Instance = this;
        }

        public void AddCoin(int value)
        {
            SaveNewCountInPlayerPrefs(CoinKey, value);

            _currentCountCoin.text = PlayerPrefs.GetInt(WoodKey, 0).ToString();
        }
        
        public void AddPlanet(int value)
        {
            SaveNewCountInPlayerPrefs(PlanetKey, value);
            
            _currentCountPlanet.text = PlayerPrefs.GetInt(PlanetKey, 0).ToString();
        }
        
        public void AddWood(int value)
        {
            SaveNewCountInPlayerPrefs(WoodKey, value);
            
            _currentCountWood.text = PlayerPrefs.GetInt(WoodKey, 0).ToString();
        }
        
        public void AddDiamond(int value)
        {
            SaveNewCountInPlayerPrefs(DiamondKey, value);
            
            _currentCountDiamond.text = PlayerPrefs.GetInt(DiamondKey, 0).ToString();
        }

        private void SaveNewCountInPlayerPrefs(string key, int value)
        {
            var currentCount = PlayerPrefs.GetInt(key, 0);
            var newCount = currentCount + value;
            PlayerPrefs.GetInt(key, newCount);
        }
    }
}