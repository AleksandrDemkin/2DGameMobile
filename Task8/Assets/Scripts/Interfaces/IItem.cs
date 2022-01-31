using System;
using UnityEngine;

namespace Items
{
    public interface IItem
    {
        public int Id { get; }
        
        public ItemInfo Info { get; }

        public Sprite UIIcon { get; }
    }
}