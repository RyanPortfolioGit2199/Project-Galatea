using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public int minDistanceFromPlayer;
    public int maxDistanceFromPlayer;
    public float fireRate;
    public float health;
    public float shield;
    


    
}
