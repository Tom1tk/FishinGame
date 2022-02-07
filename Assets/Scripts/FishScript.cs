using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public AudioSource catchSound;
    SpriteRenderer fishSprite;
    //public Animation fishAnim;
    enum fishColours { RED, YELLOW, GREEN, PURPLE, GREENL, REDL}
    [SerializeField]
    fishColours colour;
    [SerializeField]
    Sprite Red, Yellow, Green, Purple, GreenLong, RedLong;
    GameObject[] hookArr;
    public GameObject hook;
    public bool caught;
    public float moveSpeed;
    public float lifetime;
    public bool left;

    void Start()
    {
        moveSpeed = 0.6f;
        lifetime = 22f;
        left = false;
        caught = false;
        fishSprite = this.gameObject.GetComponent<SpriteRenderer>();
        catchSound = this.gameObject.GetComponent<AudioSource>();

        //fishAnim = this.gameObject.GetComponent<Animation>();
        
        pickColour();

        if(transform.position.x < 0)
        {
            fishSprite.flipX = true;
            left = true;
        }

        findHook();

        //fishAnim.Play();

        Invoke("killThisFish", lifetime);
    }

    void killThisFish()
    {
        Debug.Log("MISSED FISH KILLED");
        Destroy(this.gameObject);
    }

    void findHook()
    {
        hookArr = GameObject.FindGameObjectsWithTag("Hook");
        foreach(GameObject h in hookArr) 
        {
            hook = h;    
        }
    }

    //randomises the colour of each fish
    public void pickColour()
    {
        //selects random colour then assigns the correct sprite
        switch (Random.Range(0, 6))
        {
            case 0:
                colour = fishColours.RED;
                fishSprite.sprite = Red;
                break;
            case 1:
                colour = fishColours.YELLOW;
                fishSprite.sprite = Yellow;
                break;
            case 2:
                colour = fishColours.GREEN;
                fishSprite.sprite = Green;
                break;
            case 3:
                colour = fishColours.PURPLE;
                fishSprite.sprite = Purple;
                break;
            case 4:
                colour = fishColours.REDL;
                fishSprite.sprite = RedLong;
                break;
            case 5:
                colour = fishColours.GREENL;
                fishSprite.sprite = GreenLong;
                break;
            default:
                colour = fishColours.RED;
                fishSprite.sprite = Red;
                break;
        }
    }

    //debug methods for testing buttons to change fish colour
    public void setRed()
    {
        colour = fishColours.RED;
        fishSprite.sprite = Red;
    }
    public void setYellow()
    {
        colour = fishColours.YELLOW;
        fishSprite.sprite = Yellow;
    }
    public void setGreen()
    {
        colour = fishColours.GREEN;
        fishSprite.sprite = Green;
    }
    public void setPurple()
    {
        colour = fishColours.PURPLE;
        fishSprite.sprite = Purple;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!caught)
        {
            //simple left or right fish movement dependant on which side the fish is spawned 
            if(!left)
            {
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }else{
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag.Equals("Hook"))
        {
            //if fish collides with hook

            if(col.GetComponent<Movement>().hookedFish < 7)
            {
                //if hook is not full

                Debug.Log("FISH HAS BEEN CAUGHT");
                
                CancelInvoke("killThisFish");
                //disables this specific fish's kill timer

                caught = true;
                col.GetComponent<Movement>().hookedFish++;

                catchSound.Play();

                //this.gameObject.transform.parent = col.gameObject.transform;

                this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                this.gameObject.GetComponent<Rigidbody2D>().mass = 0.5f;
                this.gameObject.GetComponent<Rigidbody2D>().drag = 0.5f;
                //since the rigidbody type is changed during runtime, all values are set to default. Here they are set to desired values                

                this.gameObject.GetComponent<HingeJoint2D>().connectedBody = col.gameObject.GetComponent<Rigidbody2D>();
                //connects fish to hook with hinge joint so it will swing with the hooks momentum

                //puts the hinge anchor on the fishes mouth, swaps depending on which direction the sprite is facing
                if(left)
                {
                    this.gameObject.GetComponent<HingeJoint2D>().anchor = new Vector2(0.15f, 0f);
                }
                else if(!left)
                {
                    this.gameObject.GetComponent<HingeJoint2D>().anchor = new Vector2(-0.15f, 0f);
                }
            }
            

            
        }
        else if(col.gameObject.tag.Equals("Bounds"))
        {
            Debug.Log("FISH OUT OF BOUNDS");

            //could never get this collision to trigger, invoke killThisFish method does this but simpler
            Destroy(this.gameObject);
        }
    }
}
