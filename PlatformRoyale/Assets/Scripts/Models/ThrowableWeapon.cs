using UnityEngine;

public class ThrowableWeapon : Weapon
{
    public ThrowableWeapon(DropWeaponParams throwableWeaponParams) : base(throwableWeaponParams.weaponParams)
    {

    }
}

public struct DropWeaponParams
{
    public WeaponParams weaponParams;
}