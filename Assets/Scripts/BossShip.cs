using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossShip : MonoBehaviour
{
    [SerializeField] Rigidbody2D rgb;
    [SerializeField] Image heathBarFill;

    GameController _controller;
    
    public float hp = 1000;
    private float maxHp;
    void Start()
    {
        heathBarFill.enabled = false;
        rgb = GetComponent<Rigidbody2D>();
        _controller = FindObjectOfType<GameController>();
        maxHp = hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
        float targetFill = hp / maxHp;
        heathBarFill.fillAmount = targetFill;
        if (transform.position.y <=3.25)
        {
            rgb.gravityScale = 0;
            heathBarFill.enabled = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<WayPointFollower>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            _controller.isGameOver = true;
            _controller.PlaySound(3);
            Destroy(collision.gameObject);
        }
    }
}
