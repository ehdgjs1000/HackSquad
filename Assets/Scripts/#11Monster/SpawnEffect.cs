using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] Renderer _renderer;
    [SerializeField] Material mtrlOrg;
    [SerializeField] Material matrlDissolve;
    [SerializeField] float fadeTime = 0.5f;

    bool isFadeing = true;
    public float value = 0.0f;
    private void Start()
    {
        _renderer.material = matrlDissolve; 
    }
    private void Update()
    {
        if (isFadeing)
        {
            value += Time.deltaTime * fadeTime;
            _renderer.material.SetFloat("_SplitValue", value);
        }
        if (value >= fadeTime) FadeComplete();
    }

    public void StartFade()
    {
        value = 0;
        _renderer.material = matrlDissolve;
        isFadeing = true;
    }
    private void FadeComplete()
    {
        isFadeing = false;
        _renderer.material = mtrlOrg;
    }



}
