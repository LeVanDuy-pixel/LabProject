using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject explosionEffect;

    GameController _gamecontroller;
    CountTime countTime;
    private float targetAlpha = 1f;
    private float time = 20;
    private float force = 25;
    public float hp = 50f;
    void Start()
    {
        _gamecontroller = FindObjectOfType<GameController>();
        countTime = FindObjectOfType<CountTime>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (countTime.time < 50) force = 30;
        Vector3 direction = Random.insideUnitCircle;
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    private void Update()
    {
        time -= Time.deltaTime;
        if (_gamecontroller.isGameOver)
        {
            StopGame stopGame = gameObject.GetComponent<StopGame>();
            stopGame.StopGameObject();
        }

        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(color.a, targetAlpha, 0.008f);
        spriteRenderer.color = color;
        if (hp <= 0 || countTime.time < 0)
        {
            _gamecontroller.count -= 1;
            _gamecontroller.PlaySound(1);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (time <= 0)
        {
            Vector3 direction = Random.insideUnitCircle;
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * 30, ForceMode2D.Impulse);
            time = 15;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroids")
        {
            if(_gamecontroller.score > 0 && !_gamecontroller.isGameOver)
            {
                _gamecontroller.score -=0.5f;
            }
        }
        if(collision.gameObject.tag == "Player")
        {
            _gamecontroller.isGameOver = true;
            _gamecontroller.PlaySound(3);
            Destroy(collision.gameObject);
        }
    }
    

}
