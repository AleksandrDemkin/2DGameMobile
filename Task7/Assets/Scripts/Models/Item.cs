using UnityEngine;

namespace Items
{
    public class Item : IItem
    {
        public int Id { get; set; }
        
        public ItemInfo Info { get; set; }

        public Sprite UIIcon { get; }
    }
}