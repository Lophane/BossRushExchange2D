using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthCount : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject heartPrefab;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        UpdateHeartUI(playerHealth.health);
    }

    public void UpdateHeartUI(int currentHealth)
    {
        currentHealth = Mathf.Max(0, currentHealth);

        while (hearts.Count > currentHealth)
        {
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }

        while (hearts.Count < currentHealth)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart);
        }
    }

}
