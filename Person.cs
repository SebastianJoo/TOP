using System;
using System.Collections.Generic;
using System.Text;

namespace TOP
{
    class Person
    {
        public int Xposition;
        public int Yposition;
        public int MoveX;
        public int MoveY;

        public char Name { get; set; }

        public virtual int HasItems()
        {
            return 0;
        }
        public virtual Inventory GetRandom()
        {
            return null;
        }
        public virtual void TakeItem(Inventory item)
        {
        }
        public virtual void RemoveItem(Inventory item)
        {
        }
        public virtual List<Inventory> TakeAllItems()
        {
            return null;
        }
        public virtual int GetPrisonTimer()
        {
            return 0;
        }
        public virtual void IncreasePrisonTime()
        {
        }
        public virtual void ResetPrisonTime()
        {
        }
    }
}
