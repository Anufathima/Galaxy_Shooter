using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    // Start is called before the first frame update
    [SerializeField]
    private int _PowerupID;// 0= triple shot 1= speed 2= shield
    [SerializeField]
    private AudioClip _audioClip;
  
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down*Time.deltaTime* _speed);
        if (transform.position.y < -5.8f)
        {
            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_audioClip,transform.position);
           
            if (_player == null)
            {
                  Debug.Log("Player Script is null!");
            }

            else
            {
                switch (_PowerupID)
                {
                    case 0:
                        
                        _player.tripleShot();
                        break;
                    case 1:
                        
                        _player.speedup();
                        Debug.Log("speed");                        
                        break;
                    case 2:
                        
                        _player.shieldOn();
                        Debug.Log("shield");
                        break;
                    default:
                        
                        Debug.Log("default vale");
                        break;
                }
                                       
            }
            
            Destroy(gameObject);
           
            
            
        } 

    }
}
