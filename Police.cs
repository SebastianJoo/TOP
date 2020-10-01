using System;
using System.Collections.Generic;
using System.Text;

namespace TOP
{
    class Police : Person
    {
        public List<Inventory> PoliceInventory { get; set; }
        public Police(int x, int y, int moveX, int moveY)
        {
            List<Inventory> policeInventory = new List<Inventory>();
            Name = 'P';
            PoliceInventory = policeInventory;
            
            Xposition = x;
            Yposition = y;
            MoveX = moveX;
            MoveY = moveY;
        }
        public override int HasItems()
        {
            return PoliceInventory.Count;
        }
        public override void TakeItem(Inventory item)
        {
            PoliceInventory.Add(item);
        }
    }
}
