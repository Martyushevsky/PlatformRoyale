using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon(CloseWeaponParams meleeWeaponParams) : base(meleeWeaponParams.weaponParams)
    {

    }
}

public struct CloseWeaponParams
{
    public WeaponParams weaponParams;
}