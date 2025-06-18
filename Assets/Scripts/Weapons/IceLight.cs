using System.Collections;
using UnityEngine;

public class IceLight : MonoBehaviour
{
    [SerializeField] Transform iceGround;
    public float slowAmount;
    public float damage;

    private void Start()
    {
        StartCoroutine(SpawnIce());
    }
    IEnumerator SpawnIce()
    {
        yield return new WaitForSeconds(0.1f);
        Transform bullet = Instantiate(iceGround, transform.position,Quaternion.identity);
        bullet.GetComponent<IceGround>().iceDamage = damage;
        bullet.GetComponent<IceGround>().slowAmount = slowAmount;
        yield return null;
    }
    
}
