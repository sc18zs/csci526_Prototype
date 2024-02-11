using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody2D projectile;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //向后移动
        projectile.velocity = new Vector2(-speed, projectile.velocity.y);
    }
}
