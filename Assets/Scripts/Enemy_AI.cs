using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public AI_Arm armComponentRight;
    public AI_Arm armComponentLeft;

    void Start()
    {
        // Find the right and left arms using tags or naming convention
        armComponentRight = GameObject.FindGameObjectWithTag("RightArm").GetComponent<AI_Arm>();
        armComponentLeft = GameObject.FindGameObjectWithTag("LeftArm").GetComponent<AI_Arm>();
    }

    void Update()
    {
        // Right arm logic
        if (armComponentRight.currentState == AI_Arm.ArmState.Idle)
        {
            armComponentRight.UpdateState(AI_Arm.ArmState.Attacking);
        }
        if (armComponentRight.currentState == AI_Arm.ArmState.Attacking)
        {
            armComponentRight.PerformAttack();
        }

        // Left arm logic
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
