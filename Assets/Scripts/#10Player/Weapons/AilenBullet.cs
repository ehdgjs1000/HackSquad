using System.Collections;
using UnityEngine;

public class AilenBullet : MonoBehaviour
{
    public float damage;
    public float stunTime;

    public void SetBulletInfo(float _dmg, float _stunTime)
    {
        damage = _dmg;
        stunTime = _stunTime;
    }

}
