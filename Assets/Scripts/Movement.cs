using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public UIControl UICtrlRef; 
    public float speed = 10.0f;
    public float hookedFish = 0f;
    public GameObject personExerted;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 direction;

    Vector2 localDirection;

    void Start () 
    {
        rb = this.GetComponent<Rigidbody2D>();
        direction = new Vector2(0 , 1000);
        
        rb.gravityScale = 0f;
        //disables the hook movement when not in play
        hookedFish = 0f;
        personExerted.SetActive(false);
    }

    void raiseLine()
    {
        rb.AddForce(direction * speed);
        //raises the hook by a factor of "speed"
    }
    
    void FixedUpdate()
    {
        if(UICtrlRef.gamePlaying == true)
        {
            //if the game is running, enable the hook

            rb.gravityScale = 1f;

            if(Input.GetMouseButton(0))
            {
                //getmousebutton also works the same way for mobile touches, (0) being mouse/touch down
                raiseLine();
            }
        }
        else
        {
            //if the game is not running, disable the hook
            rb.gravityScale = 0f;
        }

        if(hookedFish > 4)
        {
            //if there are more than 4 fish on the hook, the fisher becomes visibly exerted
            personExerted.SetActive(true);
        }
        else
        {
            personExerted.SetActive(false);
        }
    }
}
