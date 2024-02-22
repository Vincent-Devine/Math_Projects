using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class MapDisplay : MonoBehaviour
{
    private Texture2D NoiseMapTexture;

    private MapGenerator mapGenerator;

    public Material baseMaterial;
    private Material planeMaterial;

    [Range(10U, 100U)]
    public uint Width = 50U;
    
    [Range(10U, 100U)]
    public uint Height = 50U;

    [Range(1f, 128f)]
    public List<float> Scale = new List<float>();

    [Range(1f, 100f)]
    public float HeightScale = 1f;

    public Gradient colors;

    [Range(0.2f, 2f)]
    public float FinalPow = 1.2f;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    public void CreateTexture()
    {

        mapGenerator = new MapGenerator();
        mapGenerator.CreateNoiseMap(FinalPow, Scale, Width + 1, Height + 1, new Vector2(transform.position.x, transform.position.z));

        NoiseMapTexture = new Texture2D((int)Width + 1, (int)Height + 1);

        SetPixels();

        GenerateMesh();

        planeMaterial = GetComponent<Renderer>().sharedMaterial;
        planeMaterial.mainTexture = NoiseMapTexture;

        transform.GetChild(0).position = transform.position + Vector3.up * 0.3f * HeightScale;
    }

    public void SetPixels()
    {
        for (uint y = 0U; y <= Height; ++y)
        {
            for (uint x = 0U; x <= Width; ++x)
            {
                NoiseMapTexture.SetPixel((int)x, (int)y, colors.Evaluate(mapGenerator.NoiseMap[x, y]));
            }
        }
        NoiseMapTexture.Apply();
    }

    public void GenerateMesh()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        planeMaterial = meshRenderer.sharedMaterial;
        if(planeMaterial == null)
        {
            planeMaterial = new Material(baseMaterial);
        }

        meshFilter = GetComponent<MeshFilter>();
        if(meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        Mesh mesh = new Mesh();
        mesh.name = "Grid";

        Vector3[] vertices = new Vector3[(Width + 1) * (Height + 1)];
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= Height; y++)
        {
            for (int x = 0; x <= Width; x++, i++)
            {
                vertices[i] = new Vector3(x - Width / 2f, mapGenerator.NoiseMap[x, y] * HeightScale, y - Height / 2f);
                uv[i] = new Vector2((x + Mathf.Lerp(0.5f, -0.5f, x / (float)Width)) / Width, (y + Mathf.Lerp(0.5f, -0.5f, y / (float)Height)) / Height);
            }
        }
        mesh.vertices = vertices;
        mesh.uv = uv;

        int[] triangles = new int[Width * Height * 6];

        for (int ti = 0, vi = 0, y = 0; y < Height; y++, vi++)
        {
            for (int x = 0; x < Width; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + (int)Width + 1;
                triangles[ti + 5] = vi + (int)Width + 2;
            }
        }
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        meshFilter.mesh = mesh;
    }
}
