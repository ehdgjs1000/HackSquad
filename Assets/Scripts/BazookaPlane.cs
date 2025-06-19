using UnityEngine;

public class BazookaPlane : MonoBehaviour
{
    [SerializeField] private GameObject[] wingGos;
    public float speed;
    private void Start()
    {
        Destroy(this.gameObject, 4.0f);
    }
    void Update()
    {
        for (int a = 0; a < wingGos.Length; a++)
        {
            wingGos[a].transform.Rotate(new Vector3(0, 0, 600) * Time.deltaTime);

        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        this.transform.position -= new Vector3(speed * 0.01f,0,0);
    }


}
