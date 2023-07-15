using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace DataStructures_A
{

    internal class Program
    {

        static void Main(string[] args)
        {
            //A
            //A function that accepts a multiset (which can  operationalise as an array) and a desired sum, t. Return
            // true or false to signify whether a subset that sums to t can found. Use a brute force to iterate through all possibilities.
            //Test using reltively small arrays to ensure timely completion.

            //Ask for input for set
            Console.WriteLine("Write the numbers from the subset");
            //string set_input = "1 2 3";
            string set_input = Console.ReadLine();

            //Insert elements of the set in the array
            List<int> set = set_input.Split(' ').Select(n => Convert.ToInt32(n)).ToList();

            //Total number of elements
            int totalCount = set.Count();

            //Sum requested
            Console.WriteLine("Enter requested sum: ");
            int sum = Convert.ToInt32(Console.ReadLine());

            Console.Clear();


            Console.WriteLine("Set introduced: " + set_input);
            Console.WriteLine("Number of items: " + totalCount);


            List<List<int>> possibleSubsets = GetCombinations(set);
            foreach (List<int> subset in possibleSubsets)
            {

                Console.WriteLine(string.Join(" ", subset));

            }

            Console.Clear();

            Console.WriteLine("Is there an exact sum? " + SubsetSumCheck(possibleSubsets, sum));
            Console.WriteLine("Smalles difference: " + FindSmallestDiff(possibleSubsets, sum));

            Console.ReadKey();

        }

        /// <summary>
        /// source in java: https://www.youtube.com/watch?v=NA2Oj9xqaZQ 
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        public static List<List<int>> GetCombinations(List<int> set)
        {
            //Subset empty
            int totalCount = set.Count();
            if (totalCount == 0)
            {
                return new List<List<int>> { new List<int>() };
            }

            int firstEl = set[0];
            List<int> excludeFirst = set.GetRange(1, set.Count - 1); ;//Get the rest in a list

            List<List<int>> withoutFirst = GetCombinations(excludeFirst);//Create  a list of lists without the first element of the initial array
            List<List<int>> withFirst = new List<List<int>>(); //Create a list of lists including the first element of the array

            foreach (List<int> subSet in withoutFirst) //Iterate through each of the lists without the first element
            {
                List<int> includeFirst = new List<int>(subSet); //Create a list with each "subset" ( or list) from the list of lists without first element
                includeFirst.Add(firstEl);//Add first element to the list created
                withFirst.Add(includeFirst); //Add this list to the list of lists that include the first element
            }

            //We need to concatenate the list of lists to return all the resulted lists
            List<List<int>> possibleSubsets = new List<List<int>>();

            possibleSubsets.AddRange(withFirst);
            possibleSubsets.AddRange(withoutFirst);

            return possibleSubsets;
        }
        public static bool SubsetSumCheck(List<List<int>> possibleSubsets, int sum)
        {


            bool check_sum = false;


            //if sum = 0 it is always true
            if (sum == 0)
            {
                return true;
            }
            else
            {
                for (int i = 0; i < possibleSubsets.Count; i++)
                {
                    int current_sum = possibleSubsets[i].Sum();
                    //Console.WriteLine(current_sum);
                    if (sum == current_sum)
                    {
                        check_sum = true;
                    }
                }
            }
            return check_sum;
        }
        public static int FindSmallestDiff(List<List<int>> possibleSubsets, int sum)
        {
            int difference = 0;
            List<int> differences = new List<int>();
            //Make the SubsetSumCheck useful
            //if SubsetSumCheck is true, difference is 0
            //otherwise, find the the difference
            if (SubsetSumCheck(possibleSubsets, sum) == true)
            {

                difference = 0;
            }
            else
            {
                for (int i = 0; i < possibleSubsets.Count; i++)
                {
                    int current_sum = possibleSubsets[i].Sum();
                    int temp_diff = Math.Abs(sum - current_sum);
                    if (i == 1)
                    {
                        difference = temp_diff;
                    }
                    else
                    {
                        if (temp_diff < difference)
                        {
                            difference = temp_diff;
                        }
                        else
                        {
                            difference = difference;
                        }
                    }



                }


            }

            return difference;
        }


    }
}