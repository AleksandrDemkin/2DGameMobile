using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class FightWindowView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countCrimeText;
        
        [SerializeField] private TMP_Text _countPowerEnemyText;
        
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;
        
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;
        
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;
        
        [SerializeField] private Button _addCrimeButton;
        [SerializeField] private Button _minusCrimeButton;
        
        [SerializeField] private Button _fightButton;
        
        [SerializeField] private Button _goButton;

        private int _allCountPower;
        private int _allCountHealth;
        private int _allCountMoney;
        private int _allCountCrime;
        
        private int _minCirme = 2;
        
        private Power _power;
        private Health _health;
        private Money _money;
        private Crime _crime;
        
        private Enemy _enemy;

        private void Start()
        {
            _enemy = new Enemy("Enemy Test");

            _power = new Power();
            _power.Attach(_enemy);
            
            _health = new Health();
            _health.Attach(_enemy);
            
            _money = new Money();
            _money.Attach(_enemy);
            
            _crime = new Crime();
            _crime.Attach(_enemy);
            
            _addPowerButton.onClick.AddListener(() => ChangePower(true));
            _minusPowerButton.onClick.AddListener(() => ChangePower(false));
            
            _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));
            
            _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));
            
            _addCrimeButton.onClick.AddListener(() => ChangeCrime(true));
            _minusCrimeButton.onClick.AddListener(() => ChangeCrime(false));
            
            _fightButton.onClick.AddListener(Fight);
            
            _goButton.onClick.AddListener(Rest);
        }

        private void OnDestroy()
        {
            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();
            
            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();
            
            _addMoneyButton.onClick.RemoveAllListeners();
            _minusMoneyButton.onClick.RemoveAllListeners();
            
            _addCrimeButton.onClick.RemoveAllListeners();
            _minusCrimeButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();
            
            _goButton.onClick.RemoveAllListeners();
            
            _power.Detach(_enemy);
            
            _health.Detach(_enemy);
            
            _money.Detach(_enemy);
            
            _crime.Detach(_enemy);
        }

        private void ChangePower(bool isAddCount)
        {
            if (isAddCount)
                _allCountPower++;
            else
                _allCountPower--;

            ChangeDataWindow(_allCountPower, DataType.Power);
        }
        
        private void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
                _allCountHealth++;
            else
                _allCountHealth--;
            
            ChangeDataWindow(_allCountHealth, DataType.Health);
        }
        
        private void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
                _allCountMoney++;
            else
                _allCountMoney--;
            
            ChangeDataWindow(_allCountMoney, DataType.Money);
        }
        
        private void ChangeCrime(bool isAddCount)
        {
            if (isAddCount)
                _allCountCrime++;
            else
                _allCountCrime--;
            
            ChangeDataWindow(_allCountCrime, DataType.Crime);
        }
        
        private void Fight()
        {
            Debug.Log(_allCountPower >= _enemy.Power ? "Win" : "Lose");
        }
        
        private void Rest()
        {
            var islLevelCrime = _allCountCrime >= _minCirme;
            _goButton.gameObject.SetActive(!islLevelCrime);

            Debug.Log(_allCountCrime >= _minCirme ? "Fight" : "Rest in peace and go");
        }
        
        
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Power:
                    _countPowerText.text = $"Player power: {countChangeData}";
                    _power.CountPower = countChangeData;
                    break;
                
                case DataType.Health:
                    _countHealthText.text = $"Player health: {countChangeData}";
                    _health.CountHealth = countChangeData;
                    break;
                
                case DataType.Money:
                    _countMoneyText.text = $"Player money: {countChangeData}";
                    _money.CountMoney = countChangeData;
                    break;
                
                case DataType.Crime:
                    _countCrimeText.text = $"Player crime: {countChangeData}";
                    _crime.CountCrime = countChangeData;
                    break;
            }

            _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
        }
    }
}