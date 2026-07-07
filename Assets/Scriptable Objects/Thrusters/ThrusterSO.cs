using UnityEngine;



[CreateAssetMenu(fileName = "ThrusterSO", menuName = "Scriptable Objects/ThrusterSO")]
public class ThrusterSO : ScriptableObject
{
    

[Header("Thruster ID")]
    public int ThrusterID;

    [Header("Thruster Variables")]
    public int Cost;
    public int ThrusterSpeed;
    public float DodgeRechargeRate;
    public GameObject ThrusterPrefab;
    public int dodgeAmount;
}
