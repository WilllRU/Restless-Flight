using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private float curSpeed = 0.0f;
    [SerializeField] private MeshRenderer waterRenderer;
    //[SerializeField] private Material waterMaterial;
    [SerializeField] private BirdStateMachine player;
    [SerializeField] private Transform world;
    private Vector2 dir = Vector2.zero;

    //private float speedBuildUp = 0f; 

    private Vector3 _determinedFishPos = new Vector3(0f, -10f, 590f);
    private Vector3 _determinedRockPos = new Vector3(0f, 0f, 590f);

    public Vector2 GetWorldMovement => dir;
    [SerializeField] private Vector2 scrollDir = Vector2.zero;

    private float _rockDelay = 5f;
    private float _fishDelay = 3f;

    [SerializeField] private GameObject[] fish;
    [SerializeField] private GameObject[] obstacles;

    // Use this if you want get the current speed of travel
    public float GetSpeed()
    {
        return curSpeed;
    }



    // Start is called before the first frame update
    public void StartGame()
    {
        //waterMaterial = waterRenderer.material;
        StartCoroutine(SpawnObject());
        StartCoroutine(SpawnRocks());
    }

    private void FixedUpdate()
    {
        WorldDirection();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        UpdateShaderParameters();
    }

    private void UpdateShaderParameters()
    {
        waterRenderer.material.SetVector("_ScrollDirection", new Vector2(scrollDir.x * 0.2f, scrollDir.y - 0.1f));

        float deltaFreq = curSpeed + 2f;
        //Debug.Log(deltaFreq);
        waterRenderer.material.SetFloat("_Frequency", deltaFreq);
    }

    private void WorldDirection()
    {
        curSpeed = Mathf.Lerp(curSpeed, player.SpeedMagnitude(), Time.deltaTime); // player.SpeedMagnitude();
        dir = Vector2.Lerp(dir, -player.BirdVector() * curSpeed, Time.deltaTime);
        scrollDir += dir;
        if (scrollDir.sqrMagnitude > 5f)
        {
            scrollDir = Vector2.zero;
        }
; 
    }

    private IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(_fishDelay);

        int rand = Random.Range(0, fish.Length);
        _determinedFishPos.x = Random.Range(-250, 250);

        Instantiate(fish[rand], _determinedFishPos, Quaternion.Euler(0f,0f,0f),world);
        yield return StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnRocks()
    {
        float removeAmount = 0.02f;

        yield return new WaitForSeconds(_rockDelay);

        int rand = Random.Range(0, obstacles.Length);
        _determinedRockPos.x = Random.Range(-250, 250);

        Instantiate(obstacles[rand], _determinedRockPos, Quaternion.Euler(0f, 0f, 0f), world);

        if (_rockDelay > 0.5f)
        {
            _rockDelay -= removeAmount;
        }

        yield return StartCoroutine(SpawnRocks());
    }


}
