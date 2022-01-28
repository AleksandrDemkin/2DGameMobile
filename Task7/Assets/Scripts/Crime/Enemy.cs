using Interfaces;
using Models;
using UnityEngine;

namespace Models
{
    public class Enemy : IEnemy
    {
        private string _name;
        private const int CoefPower = 2;

        private int _powerPlayer;
        private int _healthPlayer;
        private int _moneyPlayer;
        private int _crimePlayer;

        public Enemy(string name)
        {
            _name = name;
        }

        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            switch (dataType)
            {
               case DataType.Power:
                   var dataPower = (Power) dataPlayer;
                   _powerPlayer = dataPower.CountPower;
                   break;
               
               case DataType.Health:
                   var dataHealth = (Health) dataPlayer;
                   _healthPlayer = dataHealth.CountHealth;
                   break;
               
               case DataType.Money:
                   var dataMoney = (Money) dataPlayer;
                   _moneyPlayer = dataMoney.CountMoney;
                   break;
               
               case DataType.Crime:
                   var dataCrime = (Crime) dataPlayer;
                   _crimePlayer = dataCrime.CountCrime;
                   break;
            }
            
            Debug.Log($"Enemy {_name}, change {dataType}");
        }

        public int Power
        {
            get
            {
                var power = _healthPlayer + (CoefPower * (_healthPlayer + _moneyPlayer)) - _powerPlayer;
                return power;
            }
        }
        
        public int Crime => _crimePlayer;

    }
}