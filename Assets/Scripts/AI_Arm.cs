using UnityEngine;

public class AI_Arm : MonoBehaviour
{
    public int health;
    public int attackPower;
    public float attackSpeed;

    // State of the arm
    public enum ArmState { Idle, Attacking, Defending, Disabled }
    public ArmState currentState = ArmState.Idle;

    // Method to update state - can be called by the main AI script
    public void UpdateState(ArmState newState)
    {
        currentState = newState;
        // Handle state change logic
    }

    // Methods related to arm functionality
    // For example, method to perform attack
    public void PerformAttack()
    {
        // Attack logic
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            UpdateState(ArmState.Disabled);
            // Handle arm disabled logic
        }
    }

    // Additional methods as needed
}
