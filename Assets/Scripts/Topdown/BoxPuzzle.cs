using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzle : MonoBehaviour
{

    public float speed;
    public void MoveUp()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    public void MoveLeft()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    public void MoveRight()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void MoveDown()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
