using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private float curSpeed = 1.0f;
    [SerializeField] private MeshRenderer waterRenderer;
    [SerializeField] private Material waterMaterial;
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
        waterMaterial = waterRenderer.material;

    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector2.Lerp(dir, -player.BirdVelocity().normalized, Time.deltaTime);
        waterRenderer.material.SetVector("_ScrollDirection", dir);

    }
}
