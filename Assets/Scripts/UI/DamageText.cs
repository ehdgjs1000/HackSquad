using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    TextMeshPro _tmp;
    Transform _cam;

    public void Init(float damage, bool isCrit = false)
    {
        _tmp = GetComponent<TextMeshPro>();
        _cam = Camera.main?.transform;

        int dmg = Mathf.RoundToInt(damage);
        if (isCrit)
        {
            _tmp.text   = $"{dmg}!";
            _tmp.fontSize = 6f;
            _tmp.color  = new Color(1f, 0.9f, 0f);
        }
        else
        {
            _tmp.text   = dmg.ToString();
            _tmp.fontSize = 4f;
            _tmp.color  = Color.white;
        }

        StartCoroutine(FloatAndFade());
    }

    void LateUpdate()
    {
        if (_cam == null) return;
        // 카메라 방향을 향하되 world up 고정 → 카메라 기울기에 무관하게 텍스트 수직 유지
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.position, Vector3.up);
    }

    IEnumerator FloatAndFade()
    {
        float elapsed = 0f;
        const float duration = 1.0f;
        Vector3 startPos   = transform.position;
        Color   startColor = _tmp.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.position = startPos + Vector3.up * (t * 1.5f);

            float alpha = t < 0.5f ? 1f : 1f - ((t - 0.5f) / 0.5f);
            _tmp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null;
        }

        Destroy(gameObject);
    }
}
