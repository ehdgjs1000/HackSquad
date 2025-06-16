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
        // 왼쪽 마우스 버튼을 눌렀을 때
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
        if (isMoving) // 움직이고 있는 경우
        {
            // 이동할 방향으로 Time.deltaTime * 2f 의 속도로 움직임.
            transform.position += dir.normalized * Time.deltaTime * speed;
            // 현재 방향에서 움직여야할 방향으로 부드럽게 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);

            // 캐릭터의 위치와 목표 위치의 거리가 0.05f 보다 큰 동안만 이동
            isMoving = (transform.position - targetPos).magnitude > 0.05f;
        }
    }
}
