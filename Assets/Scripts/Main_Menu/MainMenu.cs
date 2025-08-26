using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
   public void SinglePlayerScene()
    {
        SceneManager.LoadScene("Single_Player");
    }
    public void CoopMode()
    {
        SceneManager.LoadScene("Co-Op_Mode");
    }
}
