using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] float deathSfxVolumn = 0.75f;
    [SerializeField] AudioClip shootSfx;
    [SerializeField] [Range(0, 1)] float shootSfxVolumn = 0.25f;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFirePeriod = 0.1f;

    Coroutine fireCoroutine;
    SceneLoader sceneLoader;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    bool touchStart = false;
    float playerMoveSpeed = 5.0f;
    Vector2 pointA;
    Vector2 pointB;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MoveWithMouse();
        Fire();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinueslyCoroutine());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinueslyCoroutine()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);

            AudioSource.PlayClipAtPoint(shootSfx, Camera.main.transform.position, shootSfxVolumn);

            yield return new WaitForSeconds(projectileFirePeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void MoveWithMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }
        if (touchStart)
        {
            Vector2 offSet = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offSet, 1.0f);
            transform.Translate((direction) * playerMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        DamageDealer damageDealer = colliderObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolumn);
        sceneLoader.LoadGameOver();
    }

    public int GetHealth()
    {
        return this.health;
    }
}
