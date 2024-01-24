using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AI_Arm armComponentRight;
    public AI_Arm armComponentLeft;

    void Start()
    {
        if (armComponentRight == null)
        {
            armComponentRight = GetComponentInChildren<AI_Arm>();
        }
        if (armComponentLeft == null)
        {
            armComponentLeft = GetComponentInChildren<AI_Arm>();
        }
    }

    void Update()
    {
        if (armComponentRight.currentState == AI_Arm.ArmState.Idle)
        {
            armComponentRight.UpdateState(AI_Arm.ArmState.Attacking);
        }

        if (armComponentRight.currentState == AI_Arm.ArmState.Attacking)
        {
            armComponentRight.PerformAttack();
        }

        if (armComponentLeft.currentState == AI_Arm.ArmState.Idle)
        {
            armComponentLeft.UpdateState(AI_Arm.ArmState.Attacking);
        }

        if (armComponentLeft.currentState == AI_Arm.ArmState.Attacking)
        {
            armComponentLeft.PerformAttack();
        }

    }
}
