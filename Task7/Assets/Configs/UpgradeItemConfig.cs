using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UpgradeItemConfig", menuName = "UpgradeItemConfig", order = 0)]

    public class UpgradeItemConfig : ScriptableObject
    {
        [SerializeField] internal ItemConfig itemConfig;

        [SerializeField] internal UpgradeType type;
        
        [SerializeField] internal float value;

        public int Id => itemConfig.Id;

        public UpgradeType UpgradeType => type;

        public float ValueUpgrade => value;
    }

    public enum UpgradeType
    {
        None,
        Speed,
        Control,
        Window,
        Armor,
        Cannon
    }
}