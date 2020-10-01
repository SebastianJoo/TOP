using System;
using System.Collections.Generic;
using System.Text;

namespace TOP
{
    class Citizen : Person

    {
        public List<Inventory> Inventory { get; set; }
        private static Random random = new Random();
        public Citizen(int x, int y, int moveX, int moveY)
        {
            List<Inventory> inventory = new List<Inventory>();
            Xposition = x;
            Yposition = y;
            MoveX = moveX;
            MoveY = moveY;
            inventory.Add(new Inventory("a phone"));
            inventory.Add(new Inventory("the keys"));
            inventory.Add(new Inventory("a watch"));
            inventory.Add(new Inventory("some money"));
            Name = 'M';
            Inventory = inventory;
        }
        public override int HasItems()
        {
            return Inventory.Count;
        }
        public override Inventory GetRandom()
        {
            return Inventory[random.Next(0, Inventory.Count)];
        }
        public override void RemoveItem(Inventory item)
        {
            Inventory.Remove(item);
        }
    }
}
