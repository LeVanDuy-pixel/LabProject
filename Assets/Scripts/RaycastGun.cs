using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RaycastGun : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Transform laserGun;

    GameController _gameController;
    public LayerMask layer;
    LineRenderer laserLine;

    public float gunRange = 10f;
    private float duration = 4; 
    private float damage = 1.5f;
    private bool shoot = true;


    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }
    private void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        if(_gameController.score >= 50)
        {
            damage = 2.5f;
            if(_gameController.score>=80)
            {
                damage = 3.5f;
                duration = 7;
                if(_gameController.score >= 100)
                {
                    damage = 5;
                    duration = 10;
                }
            }
        }
        if (_gameController.isGameOver)
        {
            _particleSystem.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            StartCoroutine(ShootLaser());
            if(shoot)
            {
                _gameController.PlaySound(4);
                shoot = false;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            DeactiveLaser();
            
        }
    }
    private void FixedUpdate()
    {
        //if(!laserLine.enabled) return;
        RaycastHit2D hit = Physics2D.Raycast(laserGun.position, Vector2.up);
        laserLine.SetPosition(0, laserGun.position);
        
        if (hit.collider!=null && hit.collider.gameObject.tag =="Asteroids" || hit.collider != null && hit.collider.gameObject.tag == "Boss")
        {
            laserLine.SetPosition(1, hit.point);
            
            if (laserLine.enabled && hit.collider.gameObject.tag == "Asteroids")
            {
                
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(-28 * hit.normal, hit.point);
                _particleSystem.transform.position = hit.point;
                _particleSystem.Play();
                Asteroids asteroids = hit.collider.gameObject.GetComponent<Asteroids>();
                asteroids.hp -= damage;
                
            }
            if(laserLine.enabled && hit.collider.gameObject.tag == "Boss")
            {
                _particleSystem.transform.position = hit.point;
                _particleSystem.Play();
                BossShip bossShip = hit.collider.gameObject.GetComponent<BossShip>();
                bossShip.hp -= damage;
            }
            
        }
        else
        {
            _particleSystem.Stop();
            laserLine.SetPosition(1, laserGun.position + Vector3.up * gunRange);
        }
        
    }
    //private void ActiveLaser()
    //{
    //    laserLine.enabled = true;
    //}
    private void DeactiveLaser()
    {
        _particleSystem.Stop();
        laserLine.enabled = false;
        laserLine.SetPosition(0, laserGun.position);
        laserLine.SetPosition(1, laserGun.position);
    }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        shoot = true;
        yield return new WaitForSeconds(duration);
        _particleSystem.Stop();
        laserLine.enabled = false;

    }
}
