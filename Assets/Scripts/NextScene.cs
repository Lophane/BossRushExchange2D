using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public GameObject reminderText;
    public string nextSceneName;

   
    void Awake()
    {
        StartCoroutine(EnableObjectAfterDelay(2.5f));
    }

    private IEnumerator EnableObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (reminderText != null)
        {
            reminderText.SetActive(true); 
        }

        StartCoroutine(WaitForInputThenLoadScene());
    }

    private IEnumerator WaitForInputThenLoadScene()
    {
        yield return new WaitUntil(() => Input.anyKeyDown);

        SceneManager.LoadScene(nextSceneName);
    }
}
