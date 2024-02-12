using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterMovement : MonoBehaviour
{
    private float moveSpeed;
    public static  float max_monster_speed = 5.2f;
    const float MIN_MONSTER_SPEED = 4.5f;

    private Rigidbody2D monster;
    public GameObject player;
    public GameObject projectile;


    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
        moveSpeed = max_monster_speed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("怪物当前最大速度为" + max_monster_speed);
        // If the monster's speed is not at the maximum value, increase the speed by 0.3 every three seconds
        if (moveSpeed < max_monster_speed)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f)
            {
                moveSpeed = Mathf.Min(moveSpeed + 0.3f, max_monster_speed);
                Debug.Log("怪兽加速" + moveSpeed);
                timer = 0.0f;
            }
        }
        monster.velocity = new Vector2(moveSpeed, monster.velocity.y);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Slow down if the monster is hit by a projectile
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("击中怪兽");
            Destroy(other.gameObject);
            // Slow down by 0.1 each time, stop when reaching the minimum speed
            if (moveSpeed >= MIN_MONSTER_SPEED)
            {
                moveSpeed -= 0.1f;
                Debug.Log("怪兽当前速度为" + moveSpeed);
            }
        }

    }

    void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }


}
