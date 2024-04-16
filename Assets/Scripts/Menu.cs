using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField] Animator transition;

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
