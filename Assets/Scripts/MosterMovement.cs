using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosterMovement : MonoBehaviour
{
    private float moveSpeed = 4.2f;
    private Rigidbody2D monster;
    public GameObject player;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        monster.velocity = new Vector2(moveSpeed, monster.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        //如果怪物追上玩家，游戏结束
        if (other.gameObject == player)
        {
            QuitGame();
        }

        //子弹击中怪兽则减速
        if (other.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("击中怪兽");
            Destroy(other.gameObject);
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
