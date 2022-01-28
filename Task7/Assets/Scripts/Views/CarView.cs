using UnityEngine;
using Upgrades;

namespace Views
{
    public class CarView : MonoBehaviour, IUpgradableCar
    {
        private readonly float _defaultSpeed;
        private readonly float _defaultArmor;

        public CarView(float speed, float armor)
        {
            _defaultSpeed = speed;
            _defaultArmor = armor;
            Restore();
        }

        public float Speed { get; set; }
        public float Control { get; set; }
        public float Window { get; set; }
        public float Armor { get; set; }
        public float Cannon { get; set; }

        public void Restore()
        {
            Speed = _defaultSpeed;
            Armor = _defaultArmor;
        }
    }
}