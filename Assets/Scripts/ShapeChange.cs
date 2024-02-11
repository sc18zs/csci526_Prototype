using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeChange : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite triangle;
    public Sprite square;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //障碍物转换为子弹
    public void ChangeToBullet()
    { 
        spriteRenderer.sprite = triangle;
        spriteRenderer.color = Color.yellow;
    }

    //子弹转换为障碍物
    public void ChangeToObstacle()
    {
        spriteRenderer.sprite = square;
        spriteRenderer.color = Color.blue;
    }
}
