using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Support class for collections & simmilar
/// </summary>
public static partial class CollectionSupport
{
    /// <summary>
    /// Checkst whether all the elements in a collection are unique or not.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="target">The collection.</param>
    /// <returns></returns>
    public static bool Unique<T>(this IEnumerable<T> target) where T: System.IComparable
    {
        /*
         * TODO: LINQ solution?
         *  - Ivan
         */
        for (int i = 0; i < target.Count(); i++)
        {
            for (int j = 0; j < target.Count(); j++)
            {
                if (i != j && target.ElementAt(i).CompareTo(target.ElementAt(j))==0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static T Random<T>(this IEnumerable<T> target)
    {
        int targetLength = target.Count();

        int randomIndex = UnityEngine.Random.Range(0, targetLength);

        return target.ElementAtOrDefault(targetLength==0 ? 0 : randomIndex);
    }


    public static IEnumerable<UnityEngine.Component> AllChildComponents(this UnityEngine.GameObject target)
    {
        return target.transform.AllChildComponents();
    }

    public static IEnumerable<UnityEngine.Component> AllChildComponents(this UnityEngine.Transform target)
    {
        return target == null ? null : target.GetComponentsInChildren<UnityEngine.Component>().AsEnumerable();
    }

    public static bool IsEmpty<T>(this IEnumerable<T> collection)
    {
        return collection.Count() == 0;
    }

    public static T ClosestTo<T, U>(this IEnumerable<T> collection, U reference)
        where T : UnityEngine.Component
        where U : UnityEngine.Component
    {
        if (collection.IsEmpty() || reference == null)
            return null;

        T closest = null;

        float closestDist = float.PositiveInfinity;
        float currentDist = float.NaN;

        foreach(var element in collection)
        {
            if (element == null)
                continue;

            currentDist = UnityEngine.Vector3.Distance(reference.transform.position,element.transform.position);

            if(currentDist<closestDist)
            {
                closest = element;
                closestDist = currentDist;
            }
        }

        return closest;
    }
}
