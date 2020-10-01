using System;
using System.Collections.Generic;
using System.Text;

namespace TOP
{
    class Thief : Person
    {
        public int TimeInPrison { get; set; }
        public List<Inventory> ThiefInventory { get; set; }

        public Thief(int x, int y, int moveX, int moveY, int timeInPrison)
        {
            Xposition = x;
            Yposition = y;
            MoveX = moveX;
            MoveY = moveY;

            Name = 'T';

            List<Inventory> thiefInventory = new List<Inventory>();
            ThiefInventory = thiefInventory;

            TimeInPrison = timeInPrison;
        }
        public override int HasItems()
        {
            return ThiefInventory.Count;
        }
        public override void TakeItem(Inventory item)
        {
            ThiefInventory.Add(item);
        }
        public override List<Inventory> TakeAllItems()
        {
            return new List<Inventory>(ThiefInventory);
        }
        public override void RemoveItem(Inventory item)
        {
            ThiefInventory.Remove(item);
        }

        public override int GetPrisonTimer()
        {
            return TimeInPrison;
        }
        public override void IncreasePrisonTime()
        {
            TimeInPrison++;
        }
        public override void ResetPrisonTime()
        {
            TimeInPrison = 0;
        }
    }
}
