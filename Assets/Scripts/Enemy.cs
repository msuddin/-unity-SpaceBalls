using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectTileSpeed = -15f;
    [SerializeField] GameObject deathVfx;
    [SerializeField] AudioClip deathSfx;
    [SerializeField] [Range(0, 1)] float deathSfxVolumn = 0.75f;
    [SerializeField] AudioClip shootSfx;
    [SerializeField] [Range(0, 1)] float shootSfxVolumn = 0.25f;
    [SerializeField] int scorePoints = 100;

    float durationOfExplosion = 1f;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectTileSpeed);

        AudioSource.PlayClipAtPoint(shootSfx, Camera.main.transform.position, shootSfxVolumn);
    }

    private void OnTriggerEnter2D(Collider2D colliderObject)
    {
        DamageDealer damageDealer = colliderObject.gameObject.GetComponent<DamageDealer>();
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
        GameObject explosion = Instantiate(this.deathVfx, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, durationOfExplosion);

        AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolumn);
        gameSession.SetPlayerScoreToIncreaseBy(this.scorePoints);
    }
}
