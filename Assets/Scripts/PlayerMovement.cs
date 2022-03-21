using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region variables
    public float speed = 3f;

    #endregion

    #region methods
    void Movement()
    {
        Vector2 moveDirection = Vector2.zero;


        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;

        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        moveDirection.Normalize();
        moveDirection *= speed * Time.deltaTime;

        transform.position += (Vector3)moveDirection;



    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="coin")
        {
            Destroy(collision.gameObject);
        }
    }
 
}
