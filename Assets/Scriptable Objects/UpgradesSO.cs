using UnityEngine;

public enum UpgradeType { Weapon, Thrusters, Shields }

[CreateAssetMenu(fileName = "UpgradesSO", menuName = "Scriptable Objects/UpgradesSO")]
public class UpgradesSO : ScriptableObject
{
    [Header("Shared Settings")]
    public int UpgradeID;
    public float Cost;

    public UpgradeType upgradeType;

    //Weapon Fields
    [Header("Weapon Variables")]
    public int Damage;
    public int ShieldDamage;
    public bool IsAutomatic = false;
    public bool CanCharge = false;
    public GameObject HitVFX;
    public float FireRate;
    public GameObject GunPrefab;


    //Thruster Fields
    [Header("Thruster Variables")]
    public int ThrusterSpeed;
    public float DodgeRechargeRate;
    public GameObject ThrusterPrefab;
    public int dodgeAmount;

    //Shield Fields
    [Header("Shield Variables")]
    public float ShieldAmount;
    public float RechargeRate;
    public GameObject ShieldPrefab;


    
    
}


