using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x += 3.0f * hori * Time.deltaTime;
        position.y += 3.0f * vert * Time.deltaTime;
        transform.position = position;
    }
}
