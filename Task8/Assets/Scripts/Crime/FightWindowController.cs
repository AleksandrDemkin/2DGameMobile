using Controllers;
using Models;
using Profile;
using UnityEngine;
using Views;

namespace Crime
{
    public class FightWindowController : BaseController
    {
        private FightWindowView _fightWindowView;
        private ProfilePlayer _profilePlayer;

        private int _allCountPower;
        private int _allCountHealth;
        private int _allCountMoney;
        private int _allCountCrime;

        private int _minCirme = 2;

        private Power _power;
        private Health _health;
        private Money _money;
        private Models.Crime _crime;

        private Enemy _enemy;

        public FightWindowController(Transform plaseForUi,
            ProfilePlayer profilePlayer, FightWindowView fightWindowView)
        {
            _profilePlayer = profilePlayer;

            _fightWindowView = Object.Instantiate(fightWindowView, plaseForUi);
            AddGameObjects(_fightWindowView.gameObject);
        }

        public void RefreshView()
        {
            _enemy = new Enemy("Enemy Test");

            _power = new Power();
            _power.Attach(_enemy);
            
            _health = new Health();
            _health.Attach(_enemy);
            
            _money = new Money();
            _money.Attach(_enemy);
            
            _crime = new Models.Crime();
            _crime.Attach(_enemy);
            
            _fightWindowView.AddPowerButton.onClick.AddListener(() => ChangePower(true));
            _fightWindowView.MinusPowerButton.onClick.AddListener(() => ChangePower(false));
            
            _fightWindowView.AddHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _fightWindowView.MinusHealthButton.onClick.AddListener(() => ChangeHealth(false));
            
            _fightWindowView.AddMoneyButton.onClick.AddListener(() => ChangeMoney(true));
            _fightWindowView.MinusMoneyButton.onClick.AddListener(() => ChangeMoney(false));
            
            _fightWindowView.AddCrimeButton.onClick.AddListener(() => ChangeCrime(true));
            _fightWindowView.MinusCrimeButton.onClick.AddListener(() => ChangeCrime(false));
            
            _fightWindowView.FightButton.onClick.AddListener(Fight);
            
            _fightWindowView.LeaveFightButton.onClick.AddListener(CloseWindow);
        }

        protected override void OnDispose()
        {
            _fightWindowView.AddPowerButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusPowerButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddHealthButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusHealthButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddMoneyButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusMoneyButton.onClick.RemoveAllListeners();
            
            _fightWindowView.AddCrimeButton.onClick.RemoveAllListeners();
            _fightWindowView.MinusCrimeButton.onClick.RemoveAllListeners();

            _fightWindowView.FightButton.onClick.RemoveAllListeners();
            
            _fightWindowView.LeaveFightButton.onClick.RemoveAllListeners();
            
            _power.Detach(_enemy);
            
            _health.Detach(_enemy);
            
            _money.Detach(_enemy);
            
            _crime.Detach(_enemy);
            
            base.OnDispose();
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
        
        private void CloseWindow()
        {
            _profilePlayer.CurrentState.Value = GameState.Game;
        }
        
        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Power:
                    _fightWindowView.CountPowerText.text = $"Player power: {countChangeData}";
                    _power.CountPower = countChangeData;
                    break;
                
                case DataType.Health:
                    _fightWindowView.CountHealthText.text = $"Player health: {countChangeData}";
                    _health.CountHealth = countChangeData;
                    break;
                
                case DataType.Money:
                    _fightWindowView.CountMoneyText.text = $"Player money: {countChangeData}";
                    _money.CountMoney = countChangeData;
                    break;
                
                case DataType.Crime:
                    _fightWindowView.CountCrimeText.text = $"Player crime: {countChangeData}";
                    _crime.CountCrime = countChangeData;
                    break;
            }

            _fightWindowView.CountPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
        }
    }
}