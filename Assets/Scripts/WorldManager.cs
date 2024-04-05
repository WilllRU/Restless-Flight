using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private float curSpeed = 0.0f;
    [SerializeField] private MeshRenderer waterRenderer;
    //[SerializeField] private Material waterMaterial;
    [SerializeField] private PlayerController player;
    private Vector2 dir = new Vector2(0f, 0f);

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

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        dir.x = Mathf.Lerp(dir.x, player.BirdVelocity().x, Time.deltaTime);
        dir.y = Mathf.Lerp(dir.y, player.BirdVelocity().y, Time.deltaTime);
        */
        //dir = Vector2.Lerp(dir, -player.BirdVector(), Time.deltaTime); // -player.BirdVector();
        dir += -player.BirdVector();
        curSpeed = Mathf.Lerp(curSpeed, player.SpeedMagnitude()* 0.01f, Time.deltaTime); // player.SpeedMagnitude();
        //Debug.Log(dir);

        //waterRenderer.material.SetVector("_ScrollDirection", new Vector2(dir.x, dir.y - 0.1f));
        //waterRenderer.material.SetTextureOffset("_FoamTexture", new Vector2(dir.x, (dir.y * curSpeed)));// * Time.realtimeSinceStartup);

        //waterRenderer.material.SetTextureOffset("_FoamTexture", new Vector2(0f, -curSpeed * Time.realtimeSinceStartup));
        //waterRenderer.material.SetTextureOffset("_FoamTexture", dir); //* Time.realtimeSinceStartup);
        waterRenderer.material.SetVector("_ScrollDirection",new Vector2(dir.x * curSpeed, (dir.y*0.001f) - 0.1f));
        //waterRenderer.material.SetFloat("_SpeedScale", curSpeed + 0.1f);



    }
}
