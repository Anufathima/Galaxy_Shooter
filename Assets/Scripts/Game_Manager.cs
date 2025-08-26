using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Manager : MonoBehaviour
{
    public bool _isGameOver;
    // Start is called before the first frame update
    [SerializeField] 
    public bool _isCoopMode;
    public static Game_Manager Instance;
    // Update is called once per frame
    public int lives = 3;
    public bool _isPaused;

    [SerializeField]
    public GameObject _pauseMenu;
    
    private Animator animator;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        animator =_pauseMenu.GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
     
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R) == true && _isGameOver == true)
        {
            lives = 3;
            if (_isCoopMode == false)
            {
                SceneManager.LoadScene("Single_Player");
            }
            else
            {
                SceneManager.LoadScene("Co-Op_Mode");
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();


        }
        if (Input.GetKeyDown(KeyCode.P)== true)
        {
            _pauseMenu.SetActive(true);
            animator.Play("Pause_Menu_anim", 0, 0f);
            animator.SetBool("isPaused", true);
            _isPaused = true;
            Time.timeScale = 0;
            
        }

    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void Resume()
    {

        _pauseMenu.SetActive(false);
        animator.SetBool("isPaused", false);
        Time.timeScale = 1.0f;
    }
}
