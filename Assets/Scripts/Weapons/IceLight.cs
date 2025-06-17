using System.Collections;
using UnityEngine;

public class IceLight : MonoBehaviour
{
    [SerializeField] Transform iceGround;

    private void Start()
    {
        StartCoroutine(SpawnIce());
    }
    IEnumerator SpawnIce()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(iceGround, transform.position,Quaternion.identity);

        yield return null;
    }
    
}
