using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private float curSpeed = 0.0f;
    [SerializeField] private MeshRenderer waterRenderer;
    //[SerializeField] private Material waterMaterial;
    [SerializeField] private BirdStateMachine player;
    private Vector2 dir = Vector2.zero;

    public Vector2 GetWorldMovement => dir;
    private Vector2 scrollDir = Vector2.zero;


    // Use this if you want get the current speed of travel
    public float GetSpeed()
    {
        return curSpeed;
    }



    // Start is called before the first frame update
    private void Awake()
    {
        //waterMaterial = waterRenderer.material;

    }

    private void FixedUpdate()
    {
        curSpeed = Mathf.Lerp(curSpeed, player.SpeedMagnitude(), Time.deltaTime); // player.SpeedMagnitude();
        dir = Vector2.Lerp(dir, -player.BirdVector() * curSpeed, Time.deltaTime);
        scrollDir += dir;
        //Debug.Log(dir);
    }





    // Update is called once per frame
    void LateUpdate()
    {
        /*
        dir.x = Mathf.Lerp(dir.x, player.BirdVelocity().x, Time.deltaTime);
        dir.y = Mathf.Lerp(dir.y, player.BirdVelocity().y, Time.deltaTime);
        */
        //dir = Vector2.Lerp(dir, -player.BirdVector(), Time.deltaTime); // -player.BirdVector();

        //waterRenderer.material.SetVector("_ScrollDirection", new Vector2(dir.x, dir.y - 0.1f));
        //waterRenderer.material.SetTextureOffset("_FoamTexture", new Vector2(dir.x, (dir.y * curSpeed)));// * Time.realtimeSinceStartup);

        //waterRenderer.material.SetTextureOffset("_FoamTexture", new Vector2(0f, -curSpeed * Time.realtimeSinceStartup));
        //waterRenderer.material.SetTextureOffset("_FoamTexture", dir); //* Time.realtimeSinceStartup);
        waterRenderer.material.SetVector("_ScrollDirection",new Vector2(scrollDir.x * 0.1f , scrollDir.y - 0.1f));

        float deltaFreq = (curSpeed) + 2f;
        //Debug.Log(deltaFreq);
        waterRenderer.material.SetFloat("_Frequency", deltaFreq);



    }
}
