using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "AbilityItem", menuName = "AbilityItem", order = 0)]
    
    public class AbilityItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig _itemConfig;

        [SerializeField] private GameObject _view;
        
        [SerializeField] internal AbilityType _type;
        
        [SerializeField] internal float value;
        
        public ItemConfig ItemConfig => _itemConfig;

        public GameObject View => _view;

        public AbilityType AbilityType => _type;
        
        public float Value => value;
        
        public int Id => _itemConfig.Id;
    }

    public enum AbilityType
    {
        None,
        Bomb,
        Gun,
        Oil,
        Acceleration
    }
}