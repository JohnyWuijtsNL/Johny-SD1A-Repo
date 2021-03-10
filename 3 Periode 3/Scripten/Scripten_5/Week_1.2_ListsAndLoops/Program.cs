using System;
using System.Collections.Generic; // add this to use lists

namespace Week_1._2_ListsAndLoops
{
    class Program
    {
        static void Main(string[] args)
        {
            #region topics
            /*
             * Lists & Loops
             * - populating a list with a loop
             * - loop through a list with a foreach
             */
            #endregion

            #region populating a list with a loop
            // ## POPULATING USING A LOOP
            // create a list, this one is empty
            var numberList = new List<int>();

            // create a loop that runs 5 times
            for (int i = 0; i < 5; i++)
            {
                // put the index number in the list
                numberList.Add(i);
            }
            // the list now contains 0, 1, 2, 3, 4

            // create an array with names
            // and an empty list
            string[] themNames = new string[] { "summit1g", "Riot Games", "Syndicate", "Shroud", "ESL_CSGO" };
            var stringList = new List<string>();

            // loop over the items in the array with a foreach
            foreach (var name in themNames)
            {
                // add the item to the list
                stringList.Add(name);
            }
            #endregion

            #region loop through list with a foreach
            // ## LOOP THROUGH A LIST
            // create a list, this one is not empty
            var theThings = new List<string>() { "derp", "dank", "bonk", "stonk"};

            // loop over the items in the list with a foreach
            foreach (var thing in theThings)
            {
                // write the content to the console
                Console.WriteLine(thing);
            }
            #endregion
        }
    }
}
