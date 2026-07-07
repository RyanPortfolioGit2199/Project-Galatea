using UnityEngine;


[CreateAssetMenu(fileName = "ShieldSO", menuName = "Scriptable Objects/ShieldSO")]
public class ShieldSO : ScriptableObject
{
    
    
    [Header("Shield ID")]
    public int ShieldID;

    [Header("Shield Variables")]
    public int Cost;
    public float ShieldAmount;
    public float RechargeRate;
    public GameObject ShieldPrfab;
}
