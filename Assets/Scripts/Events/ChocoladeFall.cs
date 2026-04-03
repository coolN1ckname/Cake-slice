using System.Collections;
using UnityEditor;
using UnityEngine;

public class ChocoladeFall : MonoBehaviour
{
    private Collider spawnArea;
    public GameObject chocolatePrefab;
    public float chanceToStart = 0.001f; //0.1%
    public float minSpawnDelay = 0.1f;
    public float maxSpawnDelay = 0.5f;
    public float duration = 5f;
    public float maxLifeTime = 7f;
    private bool isActive = false;
    private float timer;
    private Coroutine spawnCoroutine;


    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private float nextEventTime;

    void Start()
    {
        SetNextEvent();
    }

    void StartEvent()
    {
        timer = 0;
        isActive = true;
        spawnCoroutine = StartCoroutine(Spawn());
    }

    void SetNextEvent()
    {
        nextEventTime = Time.time + Random.Range(1800f, 480f);
    }

    void Update()
    {
        if (!isActive && Time.time >= nextEventTime)
        {
            StartEvent();
            SetNextEvent();
        }
    }

    public IEnumerator Spawn()
    {
        while(isActive)
        {
            GameObject prefab = chocolatePrefab;

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            GameObject cake = Instantiate(prefab, position, Quaternion.Euler(0,0,0));
            Destroy(cake, maxLifeTime);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
        
    }
}
