using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float horizontal = 0f;
    private float vertical = 0f;

    private bool onFlipMode = false;


    private bool inOppositeBackground=false;
    public GameObject background;

    private float moveSpeed = 4f;

    //定义不同形状
    public Sprite triangle;
    public Sprite square;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer spriteRendererBackground;

    //子弹
    public GameObject projectilePrefab;
    public static int countProjectile = 0;

    //获取沿路的所有物体
    ShapeChange[] items;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        spriteRendererBackground = background.GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //设置按E进入/退出翻转模式
        if (Input.GetKeyDown(KeyCode.E)) {
            onFlipMode = !onFlipMode;
        }

        //判断是否进入翻转模式
        if (onFlipMode) {
            lanchFlipMode();
            //空格键发射子弹,当子弹数量大于三个的时候可以射击
            if (Input.GetKeyDown(KeyCode.Space) && countProjectile>=3)
            {
                //launch a projectile from the player
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                countProjectile -= 3;
            }
        }
        else
        {
            offFlipMode();
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(moveSpeed, vertical * moveSpeed);
        }
    }


    //启动翻转模式
    private void lanchFlipMode() {
        //翻转按键
        ReverseVerticalInput();

        //改变背景颜色为灰色
        //检查背景是否存在
        if (background != null)
        {
            // 改变颜色
            if (spriteRendererBackground != null)
            {
                spriteRendererBackground.color = Color.grey;
            }
        }
        //子弹变成障碍物
        items = FindObjectsOfType<ShapeChange>();
        foreach (ShapeChange shapeChangeScript in items)
        {
            shapeChangeScript.ChangeToObstacle();
        }
    }


    //关闭翻转模式
    private void offFlipMode() {
        //改变背景颜色为黑色
        if (background != null)
        {
            // 改变颜色
            if (spriteRendererBackground != null)
            {
                spriteRendererBackground.color = Color.black;
            }
        }

        //障碍物变成子弹
        items = FindObjectsOfType<ShapeChange>();
        foreach (ShapeChange shapeChangeScript in items)
        {
            shapeChangeScript.ChangeToPowerUp();
        }
    }



    //物体进入物体碰撞区内调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        //判断物体类型
        SpriteRenderer colliderSpriteRender = other.GetComponent<SpriteRenderer>();
        //收集子弹
        if (colliderSpriteRender.sprite == triangle)
        {
            Destroy(other.gameObject);
            //累计奖励数量加一
            countProjectile += 1;
            Debug.Log("奖励累计" + countProjectile);
        }
        //碰到障碍
        if (colliderSpriteRender.sprite == square)
        {
            Debug.Log("碰到障碍物");
            Destroy(other.gameObject);
            QuitGame();
        }
    }

    void ReverseVerticalInput()
    {
        // 反转垂直输入
        float verticalInput = Input.GetAxisRaw("Vertical");
        verticalInput *= -1f;

        // 设置刚体的速度
        rb.velocity = new Vector2(moveSpeed, verticalInput * moveSpeed);
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
