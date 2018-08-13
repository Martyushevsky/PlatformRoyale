using UnityEngine;

public class Weapon : Item
{
    public Weapon(WeaponParams weaponParams) : base(weaponParams.itemParams)
    {

    }
}

public struct WeaponParams
{
    public ItemParams itemParams;
}