using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private BirdStateMachine _bird;
    private CameraFollow _cam;
    private WorldManager _worldMan;
    [SerializeField] private GameObject _energy;


    private void Awake()
    {
        _cam = FindObjectOfType<CameraFollow>();
        _worldMan = FindObjectOfType<WorldManager>();

        _bird = FindObjectOfType<BirdStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(StartSequence());
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator StartSequence()
    {
        _bird.GetRigidbody.isKinematic = false;
        _bird.enabled = true;

        _energy.SetActive(true);

        _cam.enabled = true;
        _worldMan.StartGame();

        this.gameObject.SetActive(false);
        yield return null;
    }

}
