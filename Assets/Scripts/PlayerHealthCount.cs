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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                UpdateHeartUI(playerHealth.health);
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player object.");
            }
        }
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
