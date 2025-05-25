using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FigureSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private FigurineFactory factory;
    [SerializeField] private ShapeType shape;
    [SerializeField] private BackColor color;
    [SerializeField] private AnimalType animal;
    [SerializeField] private float spawnInterval = 0.2f;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 areaCenter;
    [SerializeField] private Vector3 areaSize;

    private List<GameObject> allSpawnedFigurines;
    private int initUniqueSpawnCount = 3;
    private List<FigurineData> figurines;
    private GameSettings _gameSettings;
    private ActionBar _actionBar;

    [Inject]
    public void Construct(GameSettings gameSettings, ActionBar actionBar)
    {
        _gameSettings = gameSettings;
        _actionBar = actionBar;
    }

    private IEnumerator SpawnFigurinesCoroutine(int uniqueCount)
    {
        var generator = new FigurineDataGenerator();
        figurines = generator.Generate(uniqueCount);
        for (int i = 0; i < figurines.Count; i++)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2),
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );
            Vector3 spawnPos = areaCenter + randomOffset;
            var fig = factory.CreateFigurine(figurines[i], spawnPos);
            allSpawnedFigurines.Add(fig);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void RespawnFigurines()
    {
        for (int i = 0;i < allSpawnedFigurines.Count;i++)
        {
            if (allSpawnedFigurines[i] != null)
                Destroy(allSpawnedFigurines[i].gameObject);
        }
        StartCoroutine(SpawnFigurinesCoroutine(_actionBar.ReturnRemainingUnique()));
        _actionBar.Reset();
    }

    internal void StartGame()
    {
        allSpawnedFigurines = new List<GameObject>();
        initUniqueSpawnCount = _gameSettings.uniqueFirurinesCount;
        figurines = new List<FigurineData>();
        StartCoroutine(SpawnFigurinesCoroutine(initUniqueSpawnCount));
    }
}
