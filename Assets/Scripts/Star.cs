using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] GameObject collectEffect;
    

    GameController _gameController;
    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        
        rb.AddForce(Vector2.down * 4,ForceMode2D.Impulse);
        Destroy(gameObject,4);
    }
    private void Update()
    {
        if (_gameController.isGameOver)
        {
            StopGame stopGame = gameObject.GetComponent<StopGame>();
            stopGame.StopGameObject();
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        if(collision.gameObject.tag == "Player")
        {
            _gameController.score+=1;
            _gameController.PlaySound(2);
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
