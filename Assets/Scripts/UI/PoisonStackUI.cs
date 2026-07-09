using TMPro;
using UnityEngine;

// 독 스택 UI — 몬스터 머리 위에 아이콘+스택 수 표시 (빌보드)
public class PoisonStackUI : MonoBehaviour
{
    [SerializeField] TextMeshPro stackText;
    [SerializeField] Vector3 offset = new(0f, 2f, 0f);

    Transform _target;
    Transform _cam;

    public void Init(Transform target)
    {
        _target = target;
        _cam = Camera.main?.transform;
    }

    public void SetStacks(int stacks)
    {
        if (stackText != null) stackText.text = stacks.ToString();
    }

    void LateUpdate()
    {
        if (_target == null) { Destroy(gameObject); return; }

        transform.position = _target.position + offset;

        if (_cam == null) _cam = Camera.main?.transform;
        if (_cam != null) transform.rotation = _cam.rotation;
    }
}
