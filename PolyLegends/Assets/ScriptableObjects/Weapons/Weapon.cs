using UnityEngine;

[CreateAssetMenu(menuName ="Weapon")]
public abstract class Weapon : ScriptableObject
{
    public abstract GameObject getGameObject();
}
