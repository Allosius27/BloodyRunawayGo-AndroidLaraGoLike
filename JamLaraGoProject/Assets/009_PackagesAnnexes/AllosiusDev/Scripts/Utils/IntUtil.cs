using System;
using System.Collections;
using System.Collections.Generic;

public static class IntUtil
{
    #region Fields

    private static Random random;

    #endregion

    #region Behaviour

    private static void Init()
    {
        if (random == null) random = new Random();
    }

    public static int Random(int min, int max)
    {
        Init();
        return random.Next(min, max);
    }

    public static List<T> RandomizeList<T>(List<T> list)
    {
        List<T> randomizedList = new List<T>(); 
        while (list.Count > 0)
        {
            int index = Random(0, list.Count); //pick a random item from the master list
            randomizedList.Add(list[index]); //place it at the end of the randomized list
            list.RemoveAt(index); 
        } 
        return randomizedList;
    }

    #endregion
}
