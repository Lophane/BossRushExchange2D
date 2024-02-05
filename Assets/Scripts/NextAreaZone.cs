using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NextAreaZone : MonoBehaviour
{
    public GameObject fadeOutEffect;
    public bool bossDead = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && bossDead)
        {
            ActivateFadeOutEffect();
            StartCoroutine(ProceedToNextSceneAfterDelay(3f));
        }
    }

    void ActivateFadeOutEffect()
    {
        if (fadeOutEffect != null)
        {
            fadeOutEffect.SetActive(true);
        }
        else
        {
            Debug.LogWarning("FadeOutEffect GameObject is not assigned in the inspector.");
        }
    }

    IEnumerator ProceedToNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings; // Loop back to the first scene if this is the last one
        SceneManager.LoadScene(nextSceneIndex);
    }
}
