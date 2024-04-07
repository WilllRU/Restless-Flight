using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;



    void Start()
    {
        
    }

    public void StartGame()
    {

    }

    public void EndGame()
    {
        StartCoroutine(StopGame());
    }

    private IEnumerator StopGame()
    {
        while (Time.timeScale > 0.1f)
        {
            Time.timeScale -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);

    }

}
