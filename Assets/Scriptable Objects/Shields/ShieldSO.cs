using UnityEngine;

[CreateAssetMenu(fileName = "ShieldSO", menuName = "Scriptable Objects/ShieldSO")]
public class ShieldSO : ScriptableObject
{
    public int ShieldAmount;
    public float RechargeRate;
    public GameObject ShieldPrfab;
}
