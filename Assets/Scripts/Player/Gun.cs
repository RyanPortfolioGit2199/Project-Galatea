using UnityEngine;

public class Gun : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot (WeaponSO weaponSO)
    {
        RaycastHit gunHit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out gunHit, Mathf.Infinity))
        {
            Debug.Log(gunHit.collider.name);
            Debug.DrawRay(this.transform.position, this.transform.forward);
        }
    }
}
