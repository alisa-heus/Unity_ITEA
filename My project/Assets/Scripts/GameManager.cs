using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private GameObject[] _walls;
    [SerializeField] private TMP_Text _scoreText;

    public bool timerOn;

    private float timeLeft = 50f;
    private CharacterMovement _characterMovement;

    void Start()
    {
        timerOn = true;
        _characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
        InvokeRepeating("ChangeColor", 0, 2.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                Debug.Log("Time is up! Your score is: " + _characterMovement.score);
                _timerText.text = "Score: " + _characterMovement.score;
                Invoke("ResetGame", 3f);
            }
        }
    }
    public void ChangeColor()
    {
        if(timerOn == false) { return; }
        foreach(var wall  in _walls) 
        {
            wall.GetComponent<Renderer>().material = _materials[Random.Range(0, _materials.Length)];
        }

        _characterMovement.transform.Find("Body").GetComponent<Renderer>().material = 
        _walls[Random.Range(0, _walls.Length)].GetComponent<Renderer>().material;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
