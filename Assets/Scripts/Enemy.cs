using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2;
    [Range(0f, 1f)]
    public float lootChance = 0.5f;
    public GameObject[] loot;

    Transform target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var realTarget = Vector3.MoveTowards(current: transform.position, target.position, maxDistanceDelta: speed * Time.fixedDeltaTime);
        rb.MovePosition((Vector2)realTarget);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Die();
        }
    }

    public void Die(bool dropLoot = true)
    {
        Splasher.instance.SpawnSplash(Color.red, transform.position);
        if (dropLoot && Random.value < lootChance)
        {
            var randomLoot = loot[Random.Range(0, loot.Length)];
            Instantiate(randomLoot, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}

