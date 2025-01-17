using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CoinData
{
    public GameObject coinPrefab; // Prefab uang
    public float rarity; // Semakin tinggi nilai, semakin sering muncul
}

public class UangSpawner : MonoBehaviour
{
    public CoinData[] coins; // Daftar jenis uang
    public float spawnInterval = 1.5f; // Jarak waktu antar spawn
    public float minY = -3f; // Batas bawah posisi Y
    public float maxY = 0f; // Batas atas posisi Y
    public float spawnXPosition = 12f; // Posisi X tempat uang muncul
    public float minDistance = 1.0f; // Jarak minimum antar spawn
    private List<Vector3> recentSpawnPositions = new List<Vector3>();
    private int maxStoredPositions = 10; // Maksimum posisi yang diingat


    private void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), spawnInterval, spawnInterval);
    }

    GameObject GetRandomCoin()
    {
        float totalWeight = 0;
        foreach (var coin in coins)
        {
            totalWeight += coin.rarity;
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        foreach (var coin in coins)
        {
            cumulativeWeight += coin.rarity;
            if (randomValue <= cumulativeWeight)
            {
                return coin.coinPrefab;
            }
        }

        return null; // Fallback jika tidak ada koin (tidak seharusnya terjadi)
    }

    void SpawnCoin()
    {
        if (LogicScript.playerIsAlive)
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPosition = new Vector3(spawnXPosition, randomY, 0);

            foreach (var position in recentSpawnPositions)
            {
                if (Vector3.Distance(position, spawnPosition) < minDistance)
                {
                    return; // Batalkan spawn jika terlalu dekat
                }
            }

            GameObject selectedCoin = GetRandomCoin();

            if (selectedCoin != null)
            {
                Instantiate(selectedCoin, spawnPosition, Quaternion.identity);
                recentSpawnPositions.Add(spawnPosition);

                if (recentSpawnPositions.Count > maxStoredPositions)
                {
                    recentSpawnPositions.RemoveAt(0);
                }
            }
            else
            {
                Debug.LogError("Tidak ada koin yang dipilih!");
            }
        }
    }
}
