using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float force = 50f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _loseClip;
    
    public int score = 0;
    
    private Rigidbody _playerRB;
    
    private GameManager _gameManager;
   
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            _playerRB.velocity = new Vector3(0, 0, force);
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if(Input.GetKey(KeyCode.S)) 
        {
            _playerRB.velocity = new Vector3(0, 0, -force);
            transform.localEulerAngles = new Vector3(0, -180, 0);
        }

        if(Input.GetKey(KeyCode.A)) 
        {
            _playerRB.velocity = new Vector3(-force, 0, 0);
            transform.localEulerAngles = new Vector3(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            _playerRB.velocity = new Vector3(force, 0, 0);
            transform.localEulerAngles = new Vector3(0, 90, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Renderer>().material.color == this.transform.Find("Body").GetComponent<Renderer>().material.color && _gameManager.timerOn)
        {
            score++;
            _gameManager.ChangeColor();
            Debug.Log("Score: " + score);
            _audioSource.PlayOneShot(_winClip);
        }
        else
        {
            if(collision.gameObject.name != "floor" && _gameManager.timerOn)
            {
                score--;
                if(score < 0) 
                {
                    score = 0;
                }
                Debug.Log("Wrong wall! Score: " + score);
                _audioSource.PlayOneShot(_loseClip);
            }
        }
    }
}
