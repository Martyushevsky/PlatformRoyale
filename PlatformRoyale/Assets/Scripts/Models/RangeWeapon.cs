using UnityEngine;

public class RangeWeapon : Weapon
{
    public RangeWeapon(RangeWeaponParams rangeWeaponParams) : base(rangeWeaponParams.weaponParams)
    {

    }
}

public struct RangeWeaponParams
{
    public WeaponParams weaponParams;
}
