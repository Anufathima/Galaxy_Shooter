using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private int _speedMultiplier = 2;


    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
   
    [SerializeField]
    private GameObject _shieldPowerUpPrefab;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    private float _fireRate =0.5f;
    private float _canfire = 0f;
    [SerializeField]
    private int _lives ;
    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    [SerializeField]
    private int _score;
    


    [SerializeField]
    private bool _isTripleShotActive = false ;
    [SerializeField]
    private bool _isSpeedPowerUpActive = false;
    [SerializeField]
    private bool _isShieldPowerUpActive = false;
    private AudioSource _laserAudio;
    private Animator _Playeranim;

    [SerializeField]
    public bool _player1 =false;
    [SerializeField]
    public bool _player2 =false;
   


    // Start is called before the first frame update
    void Start()
    {
        // current position for player = new position(0,0,0)

         
        _spawnManager= GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager=GameObject.Find("Canvas").GetComponent<UIManager>();
       
        _Playeranim =GetComponent<Animator>();
        if(Game_Manager.Instance._isCoopMode == false && Game_Manager.Instance!= null)
        {
            Game_Manager.Instance._isGameOver = false;
            transform.position = new Vector3(0, 0, 0);
        }
        _laserAudio = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn_Manager is null");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI_Manager is null");
        }
       


    }

    // Update is called once per frame
    void Update()
    {
        
        if (_player1 == true)
        {
            PlayerMovement();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
            {

                FireLaser();
            }
        }
        else if(_player2 == true)
        {
            PlayerMovement2();
            if (Input.GetKeyDown(KeyCode.Return) && Time.time > _canfire)
            {
                FireLaser();
            }
        }


    }
    void PlayerMovement2()
    {
        float horizontalInput = Input.GetAxis("Horizontal2");
        float verticalInput = Input.GetAxis("Vertical2");
        // new Vector3(5,0,0) * realtime;
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            _Playeranim.SetBool("_isleft", true);

        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            _Playeranim.SetBool("_isRight", true);
        }
        else
        {
            _Playeranim.SetBool("_isleft", false);
            _Playeranim.SetBool("_isRight", false);

        }
        if (_isSpeedPowerUpActive == true)
        {
            
            transform.Translate(direction * (_speed* _speedMultiplier)* Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // new Vector3(5,0,0) * realtime;
        
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (Input.GetKey(KeyCode.A) ) {
            _Playeranim.SetBool("_isleft", true);

        }
        else if(Input.GetKey(KeyCode.D))
        {
            _Playeranim.SetBool("_isRight", true);
        }
        else
        {
            _Playeranim.SetBool("_isleft", false);
            _Playeranim.SetBool("_isRight", false);

        }
        if (_isSpeedPowerUpActive == true)
        {
            
            transform.Translate(direction * (_speed* _speedMultiplier)* Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canfire = Time.time+ _fireRate;
        if(_isTripleShotActive == true)
        {
           Instantiate(_tripleShotPrefab,transform.position, Quaternion.identity);
           
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
        }
        _laserAudio.Play();
    }
    public void Damage()
    {
        _lives = Game_Manager.Instance.lives;
        string playerName = _player1 ? "Player 1" : "Player 2";
       
        if(_isShieldPowerUpActive == true)
        {
            //_shieldPowerUpPrefab.GetComponent<AudioSource>().Play();
            _isShieldPowerUpActive = false;
            _shieldPowerUpPrefab.SetActive(false);
            return;

        }
        
        Game_Manager.Instance.lives --; 
        Debug.Log(playerName + " is damaged. lives reduced to :" + _lives);
        _uiManager.UpdateLives(_lives);
        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
        
        Debug.Log("Oh no the player is hit! lives remaining :"+ _lives);
        
        if (_lives < 1)
        {
           _spawnManager.onPlayerDeath();
           Debug.Log("The Player is Dead!");

           _uiManager.GameOver();
           Destroy(gameObject);
           
        }
        
    }
    public void tripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive=false;
    }
    public void speedup()
    {
        _isSpeedPowerUpActive = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedPowerUpActive = false;
        
    }
    public void shieldOn()
    {
        _isShieldPowerUpActive = true;
        _shieldPowerUpPrefab.SetActive(true);

    }
    public void ScoreManager(int points)
    {
        _score += points;
        _uiManager.ScoreUpdate(_score);
    }
  
    
}
