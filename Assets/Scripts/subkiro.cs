using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Subkiro 
{
    public static int[] GetRandomArray(int wantedArrayLength, int arrayMaxLength)
    {

        List<int> l = new List<int>();
        int[] shuffeldAarray = new int[wantedArrayLength];

        for (int i = 0; i < arrayMaxLength; i++)
        {
            l.Add(i);
        }

        for (int i = 0; i < wantedArrayLength; i++)
        {
            int rand = Random.Range(0, l.Count);
            shuffeldAarray[i] = l[rand];
           // Debug.Log(shuffeldAarray[i]);
            l.RemoveAt(rand);
        }

        l.Clear();
        return shuffeldAarray;
    }



    //for degree - delata is Degrei  (Mathf.Deg2Rad)
    public static Vector2 rotate(Vector2 v, float delta)
    {
        delta = Mathf.Deg2Rad*delta;

        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
