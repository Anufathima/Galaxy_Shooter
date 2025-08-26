using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private Player player;
    private Animator _animator;   
    private AudioSource _explosionAudio;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _canfire;
    private float _firerate;
    

    // Start is called before the first frame update
    void Start()
    {
       
     
        _animator = GetComponent<Animator>();
        
        if (_animator == null)
        {
            Debug.Log("player is null");
        }
        _explosionAudio = GetComponent<AudioSource>();

        

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        _firerate = Random.Range(3, 7);
        if (Time.deltaTime > _canfire)
        { 
           
            _canfire = Time.deltaTime+_firerate;

            GameObject _enemyLaser=Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers= _enemyLaser.GetComponentsInChildren<Laser>();
            for(int i =0; i < lasers.Length; i++)
            {
                lasers[i].IsEnemy();
            }
        }
        

        
    }
    void CalculateMovement()
    {
        float _randRange = Random.Range(-9.6f, 9.6f);
        transform.Translate(Vector3.down * Time.deltaTime * _speed);


        if (transform.position.y < -5.22f)
        {
            transform.position = new Vector3(_randRange, 6.3f, 0);

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            player= other.GetComponent<Player>();
            string playerName = player._player1 ? "Player 1" : "Player 2";
            player.Damage();
            Debug.Log("Damaged :" + playerName);            
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject,2.8f);
            _explosionAudio.Play();           
            Debug.Log("destroy enemy");
            
        }
        else if(other.tag== "Laser")
        {

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            player = playerObject.GetComponent<Player>();
            Destroy(other.gameObject);
            Debug.Log("destroy laser");
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject,2.8f);
            _explosionAudio.Play();
            Debug.Log("destroy enemy");
            player.ScoreManager(10);
            
            
        }
    }
}
