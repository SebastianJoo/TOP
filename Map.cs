using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TOP
{
    class Map
    {
        public int MapX;
        public int MapY;

        public void PopulateCity(int mapWidth, int mapHeight, int civs, int policemen, int thieves)
        {
            MapX = mapWidth;
            MapY = mapHeight;

            List<int> thefts = new List<int>();
            List<int> arrests = new List<int>();
            List<Person> prison = new List<Person>();
            List<Person> people = new List<Person>();

            Random random = new Random();

            SetStartingPosition(civs, policemen, thieves, people, random);

            PrintAndMove(people, prison, arrests, thefts);
        }

        private void SetStartingPosition(int amountOfCitizens, int amountOfPolice, int amountOfThieves, List<Person> people, Random random)
        {
            for (int i = 0; i < amountOfThieves; i++)
            {
                Thief thief = new Thief(random.Next(MapX), random.Next(MapY), random.Next(-1, 2), random.Next(-1, 2), 0);
                people.Add(thief);

            }
            for (int i = 0; i < amountOfCitizens; i++)
            {
                Citizen citizen = new Citizen(random.Next(MapX), random.Next(MapY), random.Next(-1, 2), random.Next(-1, 2));
                people.Add(citizen);

            }
            for (int i = 0; i < amountOfPolice; i++)
            {
                Police police = new Police(random.Next(MapX), random.Next(MapY), random.Next(-1, 2), random.Next(-1, 2));
                people.Add(police);
            }
        }
        public void PrintAndMove(List<Person> people, List<Person> prison, List<int> arrests, List<int> thefts)
        {
            Random random2 = new Random();
            List<Person> outOfPrison = new List<Person>();

            while (true)
            {
                MovePeople(people);
                PrintPeople(people);

                DetectCollision(people, prison, arrests, thefts);

                foreach (var person in prison)
                {
                    if (person.GetPrisonTimer() < 100)
                    {
                        person.IncreasePrisonTime();
                        Console.SetCursorPosition(60, 27);
                        Console.WriteLine($"A thief has been in jail for {person.GetPrisonTimer()}");

                    }
                    else
                    {
                        people.Add(person);
                        outOfPrison.Add(person);
                        person.ResetPrisonTime();
                        Console.SetCursorPosition(60, 28);
                        Console.Write("A thief is released from jail.");
                        Thread.Sleep(2000);
                    }
                }
                foreach (var person in outOfPrison)
                {
                    prison.Remove(person);
                }
                Console.SetCursorPosition(0, 27);
                Thread.Sleep(50);
                Console.Clear();
            }
        }
        private static void PrintPeople(List<Person> people)
        {
            foreach (var person in people)
            {
                Console.SetCursorPosition(person.Xposition, person.Yposition);

                if (person.Name == 'P')
                {
                    Console.Write(person.Name);
                }
                else if (person.Name == 'T')
                {
                    Console.Write(person.Name);
                }
                else if (person.Name == 'M')
                {
                    Console.Write(person.Name);
                }
            }
        }
        private void MovePeople(List<Person> people)
        {
            foreach (Person person in people)
            {
                person.Yposition += person.MoveY;
                person.Xposition += person.MoveX;

                if (person.Xposition < 0)
                {
                    person.Xposition = MapX;
                }
                else if (person.Xposition > MapX)
                {
                    person.Xposition = 0;
                }
                if (person.Yposition < 0)
                { 
                    person.Yposition = MapY; 
                }
                else if (person.Yposition > MapY) 
                { 
                    person.Yposition = 0;
                }
            }
        }
        private static void DetectCollision(List<Person> people, List<Person> thievesInPrison, List<int> numberOfArrests, List<int> citizensRobbed)
        {
            List<Person> prison2 = new List<Person>();
            Console.SetCursorPosition(0, 27);

            Console.Write($"Thieves in jail: {thievesInPrison.Count}\n");
            Console.Write($"Citizens robbed: {citizensRobbed.Count}\n");
            Console.Write($"Thieves arrested: {numberOfArrests.Count}");

            foreach (var (person, person2) in from person in people                         // <- LINQ, Häftigt. VS Crashade och nu är det så här för evigt.
                                              from person2 in people
                                              where !person.Equals(person2)
                                              where person.Xposition == person2.Xposition && person.Yposition == person2.Yposition
                                              select (person, person2))
            {
                if (person is Thief && person2 is Citizen)
                {
                    if (person2.HasItems() > 0)
                    {
                        Inventory stolenItem = person2.GetRandom();
                        person.TakeItem(stolenItem);
                        person2.RemoveItem(stolenItem);
                        Console.SetCursorPosition(60, 29);
                        Console.Write($"A thief stole {stolenItem.Name} from a citizen");
                        citizensRobbed.Add(1);

                        Console.SetCursorPosition(person.Xposition, person.Yposition);
                        Console.Write("X");
                        Thread.Sleep(2000);
                        Console.SetCursorPosition(0, 27);
                    }
                }
                else if (person is Police && person2 is Thief)
                {
                    if (person2.HasItems() > 0)
                    {
                        List<Inventory> thiefInventory = person2.TakeAllItems();
                        foreach (var item in thiefInventory)
                        {
                            person.TakeItem(item);
                            person2.RemoveItem(item);
                            Console.SetCursorPosition(60, 30);
                            Console.Write($"A policeman got {item.Name} from the thief");
                            Thread.Sleep(1000);

                        }
                        numberOfArrests.Add(1);
                        prison2.Add(person2);
                        Console.SetCursorPosition(person.Xposition, person.Yposition);
                        Console.Write("X");
                        Thread.Sleep(2000);
                    }
                }
            }
            foreach (var thief in prison2)
            {
                thievesInPrison.Add(thief);
                people.Remove(thief);
            }
        }
    }
}
