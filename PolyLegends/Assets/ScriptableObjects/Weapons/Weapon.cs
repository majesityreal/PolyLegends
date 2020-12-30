using UnityEngine;

[CreateAssetMenu(menuName ="Weapon")]
public abstract class Weapon : ScriptableObject
{
    public abstract string GetWeaponType();

    public abstract GameObject getGameObject();

    public abstract Vector3 getAngleOffset();
    public abstract Vector3 getPositionOffset();

}
