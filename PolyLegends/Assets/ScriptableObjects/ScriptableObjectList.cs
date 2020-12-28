using UnityEngine;

[CreateAssetMenu(menuName = "Util/ScriptableObjectList", fileName = "New Scriptable Object List")]
public class ScriptableObjectList : ScriptableObject
{
    public ScriptableObject[] scriptableObjects;

    public ScriptableObject GetAtIndex(int i)
    {
        if (scriptableObjects[i] != null)
            return scriptableObjects[i];
        else
        {
            Debug.Log("Returned null for the Scriptable Object get at index function");
            return null;
        }
    }
}