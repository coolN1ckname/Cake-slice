using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] cakePrefabs; // Очень важно соблюдать порядок префабов
    private List<GameObject> unlockedItems = new List<GameObject>();
    public GameObject[] rottenCakesPrefabs; // тут насрать
    public bool isCholocateBought = false;
    public GameObject chocolatePrefab;
    public float chocolateChance = 0.05f;
    public float rottenChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForece = 18f;
    public float maxForce = 26f;

    public float maxLifeTime = 7f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        unlockedItems.Add(cakePrefabs[0]); // Базовый всегда должен быть доступен
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            GameObject prefab = unlockedItems[Random.Range(0, unlockedItems.Count)];

            if (Random.value < rottenChance)
            {
                prefab = rottenCakesPrefabs[Random.Range(0,rottenCakesPrefabs.Length)];
            }

            if (isCholocateBought && Random.value < chocolateChance)
            {
                prefab = chocolatePrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));

            GameObject cake = Instantiate(prefab, position, rotation);
            Destroy(cake, maxLifeTime);

            float force = Random.Range(minForece, maxForce);
            cake.GetComponent<Rigidbody>().AddForce(cake.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    public void UnlockItem(GameObject prefab)
    {
        if (!unlockedItems.Contains(prefab))
        {
            unlockedItems.Add(prefab);
        }
    }

    public void SpawnerUpgrade()
    {
        minSpawnDelay -= 0.1f;
        maxSpawnDelay -= 0.2f; //Чаще спавнятся сладости
        chocolateChance += 0.02f; // Больше шанса для шоколада
        rottenChance -= 0.2f; // меньше шансы для говна
    }
}