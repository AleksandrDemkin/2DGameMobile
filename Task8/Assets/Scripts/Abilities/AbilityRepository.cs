using System.Collections.Generic;
using Configs;
using Controllers;
using Interfaces;
using UnityEngine;

namespace Abilities
{
    public class AbilityRepository : BaseController, IRepository<int, IAbility>
    {
        public IReadOnlyDictionary<int, IAbility> Collection => _abilityMapById;

        private Dictionary<int, IAbility> _abilityMapById = new Dictionary<int, IAbility>();

        
        public AbilityRepository(List<AbilityItemConfig> configs)
        {
            PopulateItems(configs);
        }
        
        protected override void OnDispose()
        {
            _abilityMapById.Clear();
        }

        private void PopulateItems(List<AbilityItemConfig> configs)
        {
            foreach (var config in configs)
            {
                if (_abilityMapById.ContainsKey(config.Id))
                    continue;
                
                _abilityMapById.Add(config.Id, CreateAbility(config));
            }
        }

        private IAbility CreateAbility(AbilityItemConfig config)
        {
            switch (config.AbilityType)
            {
                case AbilityType.Bomb:
                    return new BombAbility(config);
                
                case AbilityType.Gun:
                    return new BombAbility(config);
                
                case AbilityType.Oil:
                    return new BombAbility(config);
                
                default:
                   Debug.LogError("Not ability type");
                   return null;
            }
        }
    }
}