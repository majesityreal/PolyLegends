using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Util/ScriptableObjectList", fileName = "New Scriptable Object List")]
public class ScriptableObjectList : ScriptableObject
{
    public ScriptableObject[] list;

    public ScriptableObject GetAtIndex(int i)
    {
        if (list[i] != null)
            return list[i];
        else
        {
            Debug.Log("Returned null for the Scriptable Object get at index function");
            return null;
        }
    }

    public ScriptableObject GetByName(string s)
    {
        if (list.Length == 0)
        {
            Debug.LogWarning("Scriptable object list was accessed with a length of 0, returning NULL");
            return null;
        }
        foreach (ScriptableObject sObject in list)
        {
            if (sObject.name == s)
            {
                return sObject;
            }
        }
        Debug.Log(s + " could not be found in the ScriptableObjectList");
        return null;
    }

}