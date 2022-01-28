using System.Collections.Generic;
using Configs;
using Controllers;
using Items;

namespace Upgrades
{
    public class UpgradeHandlersRepository : BaseController
    {
        public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItemsMapById;
        private Dictionary<int, IUpgradeCarHandler> _upgradeItemsMapById = new Dictionary<int, 
            IUpgradeCarHandler>();

        public UpgradeHandlersRepository(List<UpgradeItemConfig> upgradeItemConfigs)
        {
            PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);
        }

        protected override void OnDispose()
        {
            _upgradeItemsMapById.Clear();
            _upgradeItemsMapById = null;
        }

        private void PopulateItems(ref Dictionary<int, IUpgradeCarHandler> upgradeHandlersMapByType,
            List<UpgradeItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (upgradeHandlersMapByType.ContainsKey(config.Id))
                    continue;
                upgradeHandlersMapByType.Add(config.Id, CreateHandlerByType(config));
            }
        }

        private IUpgradeCarHandler CreateHandlerByType(UpgradeItemConfig config)
        {
            switch (config.type)
            {
                case UpgradeType.Speed:
                    return new SpeedUpgradeCarHandler(config.value);
                case UpgradeType.Control:
                    return new SpeedUpgradeCarHandler(config.value);
                case UpgradeType.Window:
                    return new SpeedUpgradeCarHandler(config.value);
                case UpgradeType.Armor:
                    return new SpeedUpgradeCarHandler(config.value);
                case UpgradeType.Cannon:
                    return new SpeedUpgradeCarHandler(config.value);
                default:
                    return StubUpgradeCarHandler.Default;
            }
        }
    }
}