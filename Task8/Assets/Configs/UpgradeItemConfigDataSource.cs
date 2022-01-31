using Items;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource", order = 0)]
    public class UpgradeItemConfigDataSource: ScriptableObject
    {
        [SerializeField] private UpgradeItemConfig[] _itemConfigs;

        public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
    }
}