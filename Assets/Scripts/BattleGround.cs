using UnityEngine;

public class BattleGround : MonoBehaviour
{
    public static BattleGround instance;

    private bool isFighting = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void StartFight()
    {
        isFighting = true;
    }


}
