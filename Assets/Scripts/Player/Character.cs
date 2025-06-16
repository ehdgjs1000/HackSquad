using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Move();
    }
    protected virtual void Move()
    {
        if (PlayerCtrl.instance.isMoving)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
    }

}
