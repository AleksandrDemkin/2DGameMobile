namespace Upgrades
{
    public class ArmorUpgradeCarHandler : IUpgradeCarHandler
    {
        private readonly float _armor;

        public ArmorUpgradeCarHandler(float armor)
        {
            _armor = armor;
        }

        public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
        {
            upgradableCar.Armor = _armor;
            return upgradableCar;
        }
    }
}