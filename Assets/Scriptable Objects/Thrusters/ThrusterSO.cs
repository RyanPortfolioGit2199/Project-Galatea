using UnityEngine;

[CreateAssetMenu(fileName = "ThrusterSO", menuName = "Scriptable Objects/ThrusterSO")]
public class ThrusterSO : ScriptableObject
{
    public int ThrusterSpeed;
    public float DodgeRechargeRate;
    public GameObject ThrusterPrefab;
}
