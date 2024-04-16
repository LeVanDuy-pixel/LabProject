using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountTime : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timeTxt;
    [SerializeField] GameObject bossAlien;

    public float time;
    GameController _controller;
   
    void Start()
    {
        _controller = FindObjectOfType<GameController>();
        bossAlien.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!_controller.isGameOver && time >0)
        {
            time -= Time.deltaTime;
            timeTxt.text = ((int)time).ToString();
        }
        if(time <= 0)
        {
            bossAlien.SetActive(true);
        }
        
    }
    
}
