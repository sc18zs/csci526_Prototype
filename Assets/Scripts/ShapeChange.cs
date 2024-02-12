using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChange : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite triangle;
    public Sprite square;
    public Sprite diamond;
    public Sprite hexagonFlatTop;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Obstacle transforms into a PowerUp (Projectiles)
    public void ChangeToPowerUp()
    { 
        spriteRenderer.sprite = triangle;
        spriteRenderer.color = Color.yellow;
    }

    // Projectiles transforms into an Obstacle
    public void ChangeToObstacle()
    {
        spriteRenderer.sprite = square;
        spriteRenderer.color = Color.blue;
    }

    // Infinite mode transformation
    public void ChangeToInfiniteObstacle()
    {
        spriteRenderer.sprite = hexagonFlatTop;
        spriteRenderer.color = Color.green;
    }

    public void ChangeToInfiniteReward()
    {
        spriteRenderer.sprite = diamond;
        spriteRenderer.color = Color.magenta;
    }
}
