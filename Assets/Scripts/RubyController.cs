using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    float speed = 23.0f;
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }
    Rigidbody2D rigi2d;

    // Start is called before the first frame update
    void Start()
    {
        rigi2d = GetComponent<Rigidbody2D>();
        currentHealth = 1;// maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 position = rigi2d.position;
        position.x += speed * hori * Time.deltaTime;
        position.y += speed * vert * Time.deltaTime;

        rigi2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
