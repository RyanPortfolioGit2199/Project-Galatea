using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int distanceFromPlayer;
    public int attackRange;
    public float fireRate;
    public float SpeedMultiplier = 1f;
    public Vector2 RunAwayDistance;
}
