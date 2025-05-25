using UnityEngine;

public class FigurineFactory:MonoBehaviour
{
    [Header("Prefab")]
    public GameObject figurinePrefab;

    [Header("Meshes by Shape")]
    public GameObject[] shapePrefabs;  

    [Header("Materials by Border Color")]
    public Color[] shapeColors;    

    [Header("Textures by Animal")]
    public Sprite[] animalSprites;      

    private Figurine figurine;

    /// <summary>
    /// Создает фигурку с указанными данными в позиции spawnPosition
    /// </summary>
    public GameObject CreateFigurine(FigurineData data, Vector3 spawnPosition)
    {
        GameObject instance = Instantiate(figurinePrefab, spawnPosition, Quaternion.identity);
        figurine = instance.GetComponent<Figurine>();
        var view = instance.GetComponent<FigurineView>();
        if (view != null)
        {
            view.ApplyData(data, shapePrefabs, shapeColors, animalSprites);
            figurine.data = data;
        }
        return instance;
    }
}