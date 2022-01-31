using Upgrades;

namespace Models
{
    public class CarModel : IUpgradableCar
    {
        public float Speed { get; set; }
        public float Control { get; set; }
        public float Window { get; set; }
        public float Armor { get; set; }
        public float Cannon { get; set; }
        public void Restore()
        {
            throw new System.NotImplementedException();
        }

        public CarModel(float speed)
        {
            Speed = speed;
        }
    }
}