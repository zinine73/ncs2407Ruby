using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigi2D;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigi2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigi2D.position;
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
        }
        else
        {
            position.x += Time.deltaTime * speed * direction;
        }

        rigi2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController ruby = 
            collision.gameObject.GetComponent<RubyController>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }
    }
}
