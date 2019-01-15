using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    public GameObject[] elementSpawnees;
    public float spawnMostWait;
    public float spawnLeastWait;
    public static bool stop;
    private int randSpawneeNumber;
    private float spawnWait;
    public GameObject[] spawnPoints;

    private List<int> numbers;

    void Start()
    {
        addRandomNumbers();
        StartCoroutine(WaitSpawner());
    }

    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(1);

        while (!stop)
        {
            if(numbers.Count <= 0){
                addRandomNumbers();
            }
            randSpawneeNumber = (int)numbers[0];numbers.RemoveAt(0);
            
            var spawnPosition = new Vector2(transform.position.x, transform.position.y - 15);
            spawnPosition.x = Random.Range(spawnPoints[0].transform.position.x, spawnPoints[1].transform.position.x);

            Instantiate(elementSpawnees[randSpawneeNumber], spawnPosition, transform.rotation, transform.parent);

            FindObjectOfType<AudioManager>().Play("spawn");

            yield return new WaitForSeconds(spawnWait);
        }
    }

    void addRandomNumbers()
    {
        numbers = new List<int>();
        for (int i = 0; i < elementSpawnees.Length; i++)
        {
            int randNum = Random.Range(0,elementSpawnees.Length);
            while (numbers.Contains(randNum))
            {
                randNum = Random.Range(0,elementSpawnees.Length);
            }
            numbers.Add(randNum);
        }
        foreach(int x in numbers){
            Debug.Log(x);
        }
    }
}
