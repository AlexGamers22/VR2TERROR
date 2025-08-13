using UnityEngine;

public class MeshToTerrain : MonoBehaviour
{
    public MeshFilter meshFilter; // El OBJ que quieres convertir
    public int terrainResolution = 256; // Resolución del mapa de alturas
    public float terrainHeight = 50f; // Altura máxima del terreno
    public int vecinosParaSuavizar = 4; // Número de vértices cercanos para suavizar

    [Header("Árboles")]
    public GameObject prefabArbol; // Prefab del árbol (debe ser compatible con Terrain)
    public int cantidadArboles = 100;

    public void ConvertMeshToTerrain()
    {
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;

        // Crear un nuevo terreno
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = terrainResolution;
        terrainData.size = meshFilter.mesh.bounds.size;

        // Crear mapa de alturas
        float[,] heights = new float[terrainResolution, terrainResolution];
        Bounds bounds = mesh.bounds;

        for (int x = 0; x < terrainResolution; x++)
        {
            for (int z = 0; z < terrainResolution; z++)
            {
                float normX = (float)x / (terrainResolution - 1);
                float normZ = (float)z / (terrainResolution - 1);

                float worldX = Mathf.Lerp(bounds.min.x, bounds.max.x, normX);
                float worldZ = Mathf.Lerp(bounds.min.z, bounds.max.z, normZ);

                float height = SampleMeshHeightSuavizado(vertices, worldX, worldZ, bounds, vecinosParaSuavizar);
                heights[z, x] = height / terrainHeight;
            }
        }

        terrainData.SetHeights(0, 0, heights);

        // Crear GameObject de Terrain
        GameObject terrainGO = Terrain.CreateTerrainGameObject(terrainData);
        terrainGO.transform.position = meshFilter.transform.position;

        // Añadir prototipo de árbol al terreno
        if (prefabArbol != null)
        {
            TreePrototype[] prototipos = new TreePrototype[1];
            prototipos[0] = new TreePrototype();
            prototipos[0].prefab = prefabArbol;

            Terrain terrain = terrainGO.GetComponent<Terrain>();
            terrain.terrainData.treePrototypes = prototipos;

            // Generar árboles en posiciones aleatorias
            for (int i = 0; i < cantidadArboles; i++)
            {
                float posX = Random.Range(0f, 1f);
                float posZ = Random.Range(0f, 1f);

                TreeInstance arbol = new TreeInstance();
                arbol.position = new Vector3(posX, 0, posZ);
                arbol.prototypeIndex = 0;
                arbol.widthScale = 1f;
                arbol.heightScale = 1f;
                arbol.color = Color.white;
                arbol.lightmapColor = Color.white;

                terrain.AddTreeInstance(arbol); // ¡Usa terrain, no terrainData!
            }
        }
    }

    // Toma el promedio de los N vértices más cercanos para suavizar
    float SampleMeshHeightSuavizado(Vector3[] vertices, float x, float z, Bounds bounds, int vecinos)
    {
        System.Collections.Generic.List<(float dist, float height)> lista = new System.Collections.Generic.List<(float, float)>();

        foreach (Vector3 vert in vertices)
        {
            float dist = Vector2.Distance(new Vector2(vert.x, vert.z), new Vector2(x, z));
            float height = vert.y - bounds.min.y;
            lista.Add((dist, height));
        }

        lista.Sort((a, b) => a.dist.CompareTo(b.dist));

        float suma = 0f;
        int cantidad = Mathf.Min(vecinos, lista.Count);
        for (int i = 0; i < cantidad; i++)
        {
            suma += lista[i].height;
        }
        return suma / cantidad;
    }
}