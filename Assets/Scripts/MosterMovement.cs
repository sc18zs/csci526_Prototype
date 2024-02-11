using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterMovement : MonoBehaviour
{
    private float moveSpeed = 4.2f;
    private Rigidbody2D monster;
    public GameObject player;
    public GameObject projectile;


    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果monster的速度未达到最高值，每三秒钟速度增加0.1
        if (moveSpeed < 4.2f)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f)
            {
                moveSpeed += 0.1f;
                Debug.Log("怪兽加速" + moveSpeed);
                timer = 0.0f;
            }
        }
        monster.velocity = new Vector2(moveSpeed, monster.velocity.y);

    }


    private void OnTriggerEnter2D(Collider2D other) {
        ////如果怪物追上玩家，游戏结束
        //if (other.gameObject == player)
        //{
        //    Debug.Log("怪物追上玩家");
        //    QuitGame();
        //}

        //子弹击中怪兽则减速
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("击中怪兽");
            Destroy(other.gameObject);
            //每一次减速0.2
            moveSpeed -= 0.1f;
            Debug.Log("怪兽当前速度为" + moveSpeed);
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
