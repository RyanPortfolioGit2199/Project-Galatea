using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSO", menuName = "Scriptable Objects/ShieldSO")]
public class ShieldSO : ScriptableObject
{
    public float ShieldAmount;
    public float RechargeRate;
    public GameObject ShieldPrfab;
}
