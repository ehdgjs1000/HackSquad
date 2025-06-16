using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;

    //About Fight Mode
    Vector3 targetPos;
    Vector3 dir;
    Quaternion lookTarget;
    public bool isMoving = false;
    public float speed;

    //About Fight Mode
    public bool isFightMode = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (!isFightMode)
        {
            Move();
        }

        
    }

    void Move()
    {
        // ���� ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                dir = targetPos - transform.position;

                lookTarget = Quaternion.LookRotation(dir);
                isMoving = true;
            }
        }
        if (isMoving) // �����̰� �ִ� ���
        {
            // �̵��� �������� Time.deltaTime * 2f �� �ӵ��� ������.
            transform.position += dir.normalized * Time.deltaTime * speed;
            // ���� ���⿡�� ���������� �������� �ε巴�� ȸ��
            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);

            // ĳ������ ��ġ�� ��ǥ ��ġ�� �Ÿ��� 0.05f ���� ū ���ȸ� �̵�
            isMoving = (transform.position - targetPos).magnitude > 0.05f;
        }
    }
}
