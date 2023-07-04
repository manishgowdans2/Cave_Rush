using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public bool canMove;
    

    public bool movingRight=true;

    public Transform groundDetect;

    private void Start()
    {
        canMove = true;
    }
    private void Update()
    {
        if (canMove)
        {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, distance, LayerMask.GetMask("enemyWalkable"));
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {
            var healthComponent = collision.GetComponent<Health>();
           
            healthComponent.takeDamage();
            /*var force = collision.transform.position - transform.position;
            force=-force.normalized;
            
            collision.GetComponent<Rigidbody2D>().AddForce(force * knockback,ForceMode2D.Impulse);*/
            
            StartCoroutine(wait());
            
        }
        if (collision.tag == "crate")
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }


    IEnumerator wait()
    {
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;

    }
}
