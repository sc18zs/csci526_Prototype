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

    public GameObject background;

    private float moveSpeed = 5f;

    //Define different shapes
    public Sprite triangle;
    public Sprite square;
    public Sprite hexagonFlat;
    private SpriteRenderer spriteRendererBackground;

    //Projectiles
    public GameObject projectilePrefab;
    public static int countProjectile = 0;
    const int PROJECTILE_COLLECTION = 3;

    //Infinite mode timer
    private float infiniteTimer = 0.0f;
    private bool infiniteMode = false;
    const float INFINITE_PERIOD = 2.0f;

    //Get all objects along the way
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
        // Set to enter/exit flip mode with E key
        if (Input.GetKeyDown(KeyCode.E)) {
            onFlipMode = !onFlipMode;
        }

        // Check if entering flip mode
        if (onFlipMode) {
            lanchFlipMode();
            // Shoot projectiles with space key, only when projectile count is greater than or equal to three
            if (Input.GetKeyDown(KeyCode.Space) && countProjectile>=3)
            {
                //launch a projectile from the player
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                countProjectile -= PROJECTILE_COLLECTION;
            }
            if (infiniteMode == true)
            {
                LaunchInfinite();
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


    // Launch flip mode
    private void lanchFlipMode() {

        ReverseVerticalInput();

        // Change background color to grey
        // Check if background exists
        if (background != null)
        {
            // Change the color
            if (spriteRendererBackground != null)
            {
                spriteRendererBackground.color = Color.grey;
            }
        }
        // Projectiles turn into obstacles
        items = FindObjectsOfType<ShapeChange>();
        foreach (ShapeChange shapeChangeScript in items)
        {
            if (shapeChangeScript.CompareTag("Diamond"))
            {
                shapeChangeScript.ChangeToInfiniteObstacle();
            }
            else
            {
                shapeChangeScript.ChangeToObstacle();
            }
        }
    }


    // Turn off flip mode
    private void offFlipMode() {
        // Change background color to black
        if (background != null)
        {
            if (spriteRendererBackground != null)
            {
                spriteRendererBackground.color = Color.black;
            }
        }

        // Obstacles turn into projectiles
        items = FindObjectsOfType<ShapeChange>();
        foreach (ShapeChange shapeChangeScript in items)
        {
            if (shapeChangeScript.CompareTag("Diamond"))
            {
                shapeChangeScript.ChangeToInfiniteReward();
            }
            else
            {
                shapeChangeScript.ChangeToPowerUp();
            }
        }
    }



    // Called when an object enters the collider area
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check the shape of the object
        SpriteRenderer colliderSpriteRender = other.GetComponent<SpriteRenderer>();
        // Collect projectiles
        if (colliderSpriteRender.sprite == triangle)
        {
            Destroy(other.gameObject);
            // Increment the reward count
            countProjectile += 1;
            Debug.Log("奖励累计" + countProjectile);
        }

        // Hit obstacle or monster catches up with the player
        if (colliderSpriteRender.sprite == square && !other.gameObject.CompareTag("Border"))
        {
            Debug.Log("碰到障碍物/怪物追上玩家");
            Destroy(other.gameObject);
            QuitGame();
        }

        //Player reach the end
        if (other.gameObject.CompareTag("Destination"))
        {
            Debug.Log("到达终点，游戏结束！");
            QuitGame();
        }

        // Infinite bullet mode
        if (other.gameObject.CompareTag("Diamond"))
        {
            infiniteTimer = INFINITE_PERIOD;
            infiniteMode = true;
            LaunchInfinite();
            Destroy(other.gameObject);
        }
        if (colliderSpriteRender.sprite == hexagonFlat)
        {
            // Increase monster's maximum speed by 0.2
            MosterMovement.max_monster_speed += 0.2f;

        }
    }


    void LaunchInfinite()
    {
        // Shoot projectiles with space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
        infiniteTimer -= Time.deltaTime;
        if (infiniteTimer <= 0.0f)
        {
            infiniteMode = false;
        }
    }



    void ReverseVerticalInput()
    {
        // Reverse vertical input
        float verticalInput = Input.GetAxisRaw("Vertical");
        verticalInput *= -1.0f;

        // Set the velocity of the object
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
