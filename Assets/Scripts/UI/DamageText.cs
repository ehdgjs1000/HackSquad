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
            _tmp.text = $"{dmg}";
            _tmp.fontSize = 6f;
            _tmp.color = new Color(1f, 0.15f, 0.1f);
            StartCoroutine(PunchAndFade());
        }
        else
        {
            _tmp.text = dmg.ToString();
            _tmp.fontSize = 4f;
            _tmp.color = Color.white;
            StartCoroutine(FloatAndFade());
        }
    }

    void LateUpdate()
    {
        if (_cam == null)
        {
            _cam = Camera.main?.transform;
            if (_cam == null) return;
        }
        // 카메라 회전과 완전히 동일하게 맞춰 항상 정면으로 보이도록 함 (완전 빌보드)
        transform.rotation = _cam.rotation;
    }

    IEnumerator FloatAndFade()
    {
        float elapsed = 0f;
        const float duration = 1.0f;
        Vector3 startPos = transform.position;
        Color startColor = _tmp.color;

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

    // 치명타: 제자리에서 scale이 순간적으로 1.4배까지 커졌다가 빠르게 페이드아웃 (위로 뜨지 않음)
    IEnumerator PunchAndFade()
    {
        const float growDuration = 0.1f;
        const float holdDuration = 0.05f;
        const float fadeDuration = 0.25f;

        Vector3 baseScale = transform.localScale;
        Color startColor = _tmp.color;

        float elapsed = 0f;
        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = baseScale * Mathf.Lerp(1f, 1.4f, elapsed / growDuration);
            yield return null;
        }
        transform.localScale = baseScale * 1.4f;

        yield return new WaitForSeconds(holdDuration);

        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / fadeDuration);
            _tmp.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
