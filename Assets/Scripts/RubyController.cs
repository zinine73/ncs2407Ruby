using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigi2d;

    // Start is called before the first frame update
    void Start()
    {
        rigi2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector2 position = rigi2d.position;
        position.x += 23.0f * hori * Time.deltaTime;
        position.y += 23.0f * vert * Time.deltaTime;

        rigi2d.MovePosition(position);
    }
}
