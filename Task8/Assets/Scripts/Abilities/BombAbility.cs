using Configs;
using UnityEngine;

namespace Abilities
{
    public class BombAbility : IAbility
    {
        private readonly AbilityItemConfig _config;


        public BombAbility(AbilityItemConfig config)
        {
            _config = config;
        }

        
        public void Apply(IAbilityActivator activator)
        {
            var bomb = Object.Instantiate(_config.View);
            var rigidbody = bomb.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(Vector2.right * _config.Value, ForceMode2D.Impulse);
        }
    }
}