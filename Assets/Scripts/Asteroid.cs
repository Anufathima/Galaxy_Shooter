using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private int _speed = 3;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;
   

    // Start is called before the first frame update
    void Start()
    {
      _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();  

    }

    // Update is called once per frame
    void Update()
    {
              
        transform.Rotate(Vector3.forward*Time.deltaTime*_speed);
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
                        
            Instantiate(_explosionPrefab,transform.position, Quaternion.identity);
            
            Destroy(other.gameObject);
            Destroy(gameObject,0.25f);
            _spawnManager.StartRoutine();
        }
    }
}
