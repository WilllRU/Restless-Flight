using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;
    private MeshFilter meshFilter;

    #region Plane Variables

    [SerializeField] Vector2 planeSize = new Vector2(1, 1);
    [SerializeField] int planeResolution = 1;

    #endregion


    private List<Vector3> vertices;
    private List<int> triangles;

    //private Vector3[] vertices;
    //private int[] triangles;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;


        //CreateShape();
        //UpdateMesh();
    }

    private void Update()
    {
        planeResolution = Mathf.Clamp(planeResolution,1,50);

        GeneratePlane(planeSize, planeResolution);
        AssignMesh();
    }




    private void GeneratePlane(Vector2 size, int resolution)
    {
        vertices = new List<Vector3>();
        float xPerStep = size.x / resolution;
        float yPerStep = size.y / resolution;
        for(int y = 0; y < resolution + 1; y++)
        {
            for (int x = 0; x < resolution + 1; x++)
            {
                vertices.Add(new Vector3(x * xPerStep, 0, y * yPerStep));
            }
        }
        triangles = new List<int>();
        for (int row = 0; row < resolution; row++)
        {
            for (int column = 0; column < resolution; column++)
            {
                int i = (row * resolution) + row + column;
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i + 1);
            }
        }

    }

    private void AssignMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
    }


    /*
    private void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 (0f,0f,0f),
            new Vector3 (0f,0f,1f),
            new Vector3 (1f,0f,0f),
            new Vector3 (1f,0f,1f)
        };

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };

    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
    */
}
