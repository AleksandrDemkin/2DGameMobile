using System.Collections.Generic;
using Interfaces;

namespace Models
{
    public class DataPlayer
    {
        private List<IEnemy> _enemies = new List<IEnemy>();

        public void Attach(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Detach(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }

        protected void Notifier(DataType dataType)
        {
            foreach (var enemy in _enemies)
                enemy.Update(this, dataType);
        }
    }
}