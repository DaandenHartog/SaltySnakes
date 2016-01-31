using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ListArrayHelper    
{
    /// <summary>
    /// Reverses the array
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">The array</param>
    /// <returns>A reverserd array</returns>
    public static T[] ReverseArray<T>(T[] array)
    {
        T[] reversedArray = new T[array.Length];

        // Reverse array
        for (int i = 0; i < array.Length; i++)
            reversedArray[array.Length - 1 - i] = array[i];

        return reversedArray;
    }

    /// <summary>
    /// Randomly shuffles the array
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">Array which will be shuffled</param>
    /// <returns></returns>
    public static T[] ShuffleArray<T>(T[] array)
    {
        List<T> list = ArrayToList<T>(array);

        // Randomly shuffles the list we've received
        for (int i = 0; i < array.Length; i++)
        {
            int randomNumber = Random.Range(0, list.Count);

            array[i] = list[randomNumber];
            list.RemoveAt(randomNumber);
        }

        return array;
    }

    /// <summary>
    /// Adds a value to the array
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">Array to which will be added</param>
    /// <param name="value">Value which will be added to the array</param>
    /// <returns></returns>
    public static T[] AddToArray<T>(T[] array, T value)
    {
        // Convert the array to a list
        List<T> newList = ArrayToList<T>(array);

        // Add the value
        newList.Add(value);

        // Convert it back to an array and return it
        return ListToArray<T>(newList);
    }

    /// <summary>
    /// Adds two arrays of the same type
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">Original array</param>
    /// <param name="extraArray">Array to be added</param>
    /// <returns></returns>
    public static T[] AddToArray<T>(T[] array, params T[] values)
    {
        // Loop through all the values and add these
        for (int i = 0; i < values.Length; i++)
            array = AddToArray<T>(array, values[i]);

        return array;
    }

    /// <summary>
    /// Adds multiple arrays of the same type
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">Original array</param>
    /// <param name="extraArray">Arrays to be added</param>
    /// <returns></returns>
    public static T[] AddToArray<T>(T[] array, params T[][] addedArrays)
    {
        // Loop through all the extra arrays, and make new ones
        for (int i = 0; i < addedArrays.Length; i++)
            array = AddToArray<T>(array, addedArrays[i]);

        return array;
    }

    /// <summary>
    /// Converts an list to an array
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="list">List which will be converted</param>
    /// <returns></returns>
    public static T[] ListToArray<T>(List<T> list)
    {
        T[] array = new T[list.Count];

        for (int i = 0; i < array.Length; i++)
            array[i] = list[i];

        return array;
    }

    /// <summary>
    /// Reverses the list
    /// </summary>
    /// <typeparam name="T">Type of list</typeparam>
    /// <param name="list">List you want reversed</param>
    /// <returns></returns>
    public static List<T> ReverseList<T>(List<T> list)
    {
        List<T> reversedList = new List<T>();

        // Add values to the reversedList in reverse
        for (int i = 0; i < list.Count; i++)
            reversedList.Add(list[(list.Count - 1) - i]);

        return reversedList;
    }

    /// <summary>
    /// Randomly shuffles the list
    /// </summary>
    /// <typeparam name="T">Type of list</typeparam>
    /// <param name="list">List which will be shuffled</param>
    /// <returns></returns>
    public static List<T> ShuffleList<T> (List<T> list)
    {
        List<T> shuffledList = new List<T>();

        // Randomly shuffles the list we've received
        for (int i = 0; i < list.Count; i++ )
        {
            int randomNumber = Random.Range(0, list.Count);

            shuffledList.Add(list[randomNumber]);
            list.RemoveAt(randomNumber);
        }

        return shuffledList;
    }

    /// <summary>
    /// Adds a value to a list
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="list">List to which will be added</param>
    /// <param name="value">Value which will be added to the list</param>
    /// <returns></returns>
    public static List<T> AddToList<T>(List<T> list, T value)
    {
        // Add the value
        list.Add(value);

        return list;
    }

    /// <summary>
    /// Adds values to a list
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="list">Original list</param>
    /// <param name="values">values to be added</param>
    /// <returns></returns>
    public static List<T> AddToList<T>(List<T> list, params T[] values)
    {
        // Loop through all the values and add these
        for (int i = 0; i < values.Length; i++)
            list = AddToList<T>(list, values[i]);

        return list;
    }

    /// <summary>
    /// Adds multiple arrays of the same type
    /// </summary>
    /// <typeparam name="T">Type of array</typeparam>
    /// <param name="array">Original array</param>
    /// <param name="extraArray">Arrays to be added</param>
    /// <returns></returns>
    public static List<T> AddToList<T>(List<T> list, params List<T>[] addedLists)
    {
        // Loop through all the extra arrays, and make new ones
        for (int i = 0; i < addedLists.Length; i++)
            list = AddToList<T>(list, addedLists[i]);

        return list;
    }

    /// <summary>
    /// Converts an array to a list
    /// </summary>
    /// <typeparam name="T">Type of the array</typeparam>
    /// <param name="array">Array which will be changed to a list</param>
    /// <returns></returns>
    public static List<T> ArrayToList<T>(T[] array)
    {
        List<T> list = new List<T>();

        // Loop through the array and add the values to the list
        for (int i = 0; i < array.Length; i++)
            list.Add(array[i]);

        return list;
    }
}
