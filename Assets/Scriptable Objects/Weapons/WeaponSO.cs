using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public int Damage;
    public int ShieldDamage;
    public bool IsAutomatic = false;
    public bool CanCharge = false;
    public GameObject HitVFX;
    public float FireRate;
    public GameObject GunPrefab;
}
