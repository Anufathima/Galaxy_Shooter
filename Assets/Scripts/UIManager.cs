using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
   
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _RestartLevelText;
    
    
    private GameObject _backToMenu;
   

    // Start is called before the first frame update
    void Start()
    {

        _gameOverText.enabled = false;


      
        _backToMenu = transform.Find("Back_To_Menu").gameObject;
        

        
        
        

    }

    // Update is called once per frame

    public void ScoreUpdate(int PlayerScore)
    {
        _scoreText.text = "Score : " + PlayerScore.ToString();
    }
    public void UpdateLives(int currentlives)
    {
        if (_livesSprite != null)
        {
           
          
           _livesImg.sprite = _livesSprite[currentlives];
               
            
        }

    }
    public void GameOver()
    {
        
        Game_Manager.Instance.GameOver();
        _backToMenu.SetActive(true);
        StartCoroutine(Flicker());
        //_RestartLevelText.enabled = true;
       
    }
    IEnumerator Flicker()
    {
        while (true)
        {
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {

        Game_Manager.Instance.lives = 3;
        if(Game_Manager.Instance._isCoopMode == true && Game_Manager.Instance._isGameOver)
        {
            SceneManager.LoadScene("Co-Op_Mode");
        }
        else
        {
            SceneManager.LoadScene("Single_Player");
        }
    }
   
   


}
