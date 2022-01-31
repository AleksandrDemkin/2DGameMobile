namespace Upgrades
{
    public interface IUpgradableCar
    {
        float Speed { get; set; }
        float Control { get; set; }
        float Window { get; set; }
        float Armor { get; set; }
        float Cannon { get; set; }

        void Restore();
    }
}