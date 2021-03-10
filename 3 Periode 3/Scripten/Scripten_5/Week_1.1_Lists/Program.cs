using System;
using System.Collections.Generic;  // add this to use lists

namespace Week_1._1_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            #region topic
            /*
             * Lists: create, modify and order
             * 
             * - add and remove items
             * - add and remove collections
             * - empy lists
             * - sort lists
             *
             * A list is similar to an array
             * it is a data object for storing a number of objects of the same type
             * an array has a fixed size
             * while a list has a dynamic size
             * we use lists when the amount of items we need to store is not clear from the start
             */
            #endregion

            #region creat a list
            // ## CREATE A LIST
            // Before you create a list you need to add list functionality to the project
            // to do this add the following to the top of your file
            // using System.Collections.Generic;

            // To create a list you can use the following code...
            var numbers = new List<int>();
            // var : is a generic variable type
            // numbert : is the name you give our list
            // new List<> : initialise a new instance of the list class
            // <int> : specifies the type of items you want the list to contain
            // () : lets you set a specific size for the list

            // If you know what you want in your list to contain initially 
            // you can initialise it using {}
            var moreNumbers = new List<int>() { 1, 2, 3, 4 };
            #endregion

            #region add item to list
            // ## ADD ITEMS TO A LIST
            // to add an item to a list you can use the .Add() method that is available for the list class
            var addItemToList = new List<string>() { "GME", "AMC", "BB", "NOK" };

            addItemToList.Add("PLTR");
            addItemToList.Add("TSLA");
            #endregion

            #region add collection to list
            // ## ADD COLLECTION TO A LIST
            // to add items to a list you can use the .AddRange() method that is available for the list class
            var addCollectionToList = new List<int>();
            Console.WriteLine(addCollectionToList.Count); // print the amount of items in the list

            // you can add an array
            int[] numbs = new int[] { 4, 8 };
            addCollectionToList.AddRange(numbs);
            Console.WriteLine(addCollectionToList.Count);

            // or directly
            addCollectionToList.AddRange(new int[2] { 15, 16 });
            Console.WriteLine(addCollectionToList.Count);

            // or add another list to the list
            var numsList = new List<int>() { 23, 24 };
            addCollectionToList.AddRange(numsList);
            Console.WriteLine(addCollectionToList.Count);
            #endregion

            #region check if lists contains an item
            // ## DOES LIST CONTAIN ITEM
            // to check if an item exists in a list you can use the .Contains() method that is available for the list class
            var contiansList = new List<string> { "monkey", "donkey", "duck"};
            Console.WriteLine("Does the list contain donkey? : " + contiansList.Contains("donkey"));
            #endregion

            #region remove item from list
            // ## REMOVE ITEMS FROM A LIST
            // to remove an item from a list you can use the .Remove() method that is available for the list class
            var removeItemFromList = new List<int>() { 69, 420 };

            // remove by value
            removeItemFromList.Remove(420);
            // note that this only removes the first instance
            // when you want to remove all the instances use a for loop
            // a foreach loop will nit work because C# doe not allow you
            // to modify a collection with a foreach

            // remove by index
            removeItemFromList.RemoveAt(0);
            #endregion

            #region remove collection from list
            // ## REMOVE COLLECTION FROM A LIST
            // to remove multiple items from a list you can use the .RemoveRange() method that is available for the list class
            var rempoveCollectionFromList = new List<int>() { 3, 5, 7, 3, 0, 2, 1 };
            // remove a range of items by index
            // this removes the first three items
            rempoveCollectionFromList.RemoveRange(0, 2); 
            #endregion

            #region empty list
            // ## EMPTY A LIST
            // to empty a list you can use the .Clear() method that is available for the list class
            var emptyList = new List<string>() { "DOGE", "BTC", "ETH" };
            emptyList.Clear();
            Console.WriteLine(emptyList.Count);
            #endregion

            #region sort list
            // ## SORT A LIST
            // to sort items in a list you can use the .Sort() method that is available for the list class
            // this sorts in ascending order
            var sortList = new List<int>() { 5, 2, 6, 4, 1, 3, 7 };
            sortList.Sort();

            // to sort descending you can first sort
            // and then reverse the list order
            sortList.Reverse();
            #endregion
        }
    }
}
