using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerUpPrefab;
   
    // Start is called before the first frame update

    public void StartRoutine()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.6f, 9.6f), 6.3f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f,7f));

        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {

            int random_powerUp = Random.Range(0, 3);
            Vector3 posToSpawn = new Vector3(Random.Range(-9.6f, 9.6f), 6.3f, 0);
            Instantiate(_powerUpPrefab[random_powerUp], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(7.0f);
        }
    }
   public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

}
