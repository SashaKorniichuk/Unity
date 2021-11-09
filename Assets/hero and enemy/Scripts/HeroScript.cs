using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroScript : MonoBehaviour
{
    public float speed;
    public float Impulse;
    public Sprite[] sprites = new Sprite[5];
    public int directionInput;
    int directionJump;
    public AudioClip EnemyAudio;
    public AudioClip DeadAudio;
    public AudioClip JumpAudio;
    AudioSource Audio;
    Rigidbody2D rb;
    bool isGrounded;
    string color;
    public bool isEnemy = false;
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
        color = "White";
        isEnemy = false;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * directionInput, rb.velocity.y); 
        OnClickJump();
    }
   
    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
    }
    public void MoveJump(int jump)
    {
        directionJump = jump;
    }
    //void Flip()
    //{
    //    facingRight = !facingRight;
    //    Vector3 theScale = transform.localScale;
    //    theScale.x *= -1;
    //    transform.localScale = theScale;
    //}
    public void OnClickJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, Impulse * directionJump);

            if (directionJump == 1)
                Audio.PlayOneShot(JumpAudio);
        }
    }
    private void checkKey(Collider2D collider)
    {
        if (collider.gameObject.tag == "keyYellow")
        {
            // gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
            color = "Yellow";
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "keyBlue")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[2];
            color = "Blue";
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "keyGreen")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[3];
            color = "Green";
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "keyRed")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[4];
            color = "Red";
            Destroy(collider.gameObject);
        }
    }
    private void checkBox(Collision2D collision)
    {
        if (collision.gameObject.tag == "BoxYellow" && color == "Yellow")
        {
            Destroy(collision.gameObject);
            color = "White";
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (collision.gameObject.tag == "BoxBlue" && color == "Blue")
        {
            Destroy(collision.gameObject);
            color = "White";
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (collision.gameObject.tag == "BoxGreen" && color == "Green")
        {
            Destroy(collision.gameObject);
            color = "White";
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];

        }
        else if (collision.gameObject.tag == "BoxRed" && color == "Red")
        {
            Destroy(collision.gameObject);
            color = "White";
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];

        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        checkKey(collider);

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Enemy")
        {
            Audio.PlayOneShot(EnemyAudio);
            isEnemy = true;

        }
        if (collider.gameObject.tag == "MoveEnemy" && Mathf.Abs(transform.position.x - collider.transform.position.x) < 0.35f)
        {
            Audio.PlayOneShot(EnemyAudio);
            isEnemy = false;

        }
        else if (collider.gameObject.tag == "MoveEnemy" && Mathf.Abs(transform.position.x - collider.transform.position.x) > 0.35f)
        {
            Audio.PlayOneShot(EnemyAudio);
            isEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collider.gameObject.tag == "Enemy")
        {
            isEnemy = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        checkBox(collision);
        if (collision.gameObject.tag == "DownBarer")
        {
            Audio.PlayOneShot(DeadAudio);
            isEnemy = true;
        }
        if (collision.gameObject.tag == "Finish")
        {
            LevelController.instance.isEndGame();
        }
        if (collision.gameObject.tag == "thorns")
        {
            Audio.PlayOneShot(DeadAudio);
            isEnemy = true;

        }
        if (collision.gameObject.name.Equals("MovePlatform"))
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("MovePlatform"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.tag == "thorns")
        {
            isEnemy = false;

        }
        if (collision.gameObject.tag == "DownBarer")
        {
            isEnemy = false;
        }
    }
}
