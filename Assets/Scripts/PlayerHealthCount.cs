using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHealthCount : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject heartPrefab1;
    public GameObject heartPrefab2;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        UpdateHeartUI(playerHealth.health);
        Debug.Log("Player Health is " + playerHealth.health);
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
            GameObject selectedPrefab = Random.Range(0, 2) == 0 ? heartPrefab1 : heartPrefab2;
            GameObject heart = Instantiate(selectedPrefab, transform);
            hearts.Add(heart);
        }
    }
}
