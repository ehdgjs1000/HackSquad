using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] Material sceneTransitionMat;
    [SerializeField] float transitionTime = 1.0f;
    [SerializeField] string propertyName = "_Progress";
    public UnityEvent OnTransitionDone;

    [SerializeField] bool transitionType;
    private void Start()
    {
        if(!transitionType) StartCoroutine(TransitionCoroutine());
    }
    public IEnumerator TransitionCoroutine()
    {
        if (transitionType == true)
        {
            yield return new WaitForSeconds(0.5f);
            float currentTime = 1;
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                sceneTransitionMat.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
                yield return null;
            }
            OnTransitionDone?.Invoke();          
        }
        else
        {
            float currentTime = 0;
            while (currentTime < 1.5f)
            {
                currentTime += Time.deltaTime;
                sceneTransitionMat.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
                yield return null;
            }
            OnTransitionDone?.Invoke();
        }
        
    }

}
