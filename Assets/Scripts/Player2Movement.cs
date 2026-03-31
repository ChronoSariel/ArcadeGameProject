using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private AudioSource playerAudio;
    public AudioClip playerJump;
    public AudioClip collectCoin;
    public AudioClip playerHurt;
    public AudioClip collectPowerUp;
    public float acceleration = 0.046875f;
    public float deceleration = 0.5f;
    public float friction = 0.046875f;
    public float maxSpeed = 6.0f;
    public float groundSpeed;
    public float jumpHeight = 4f;
    public float knockbackStrength = 1.0f;
    public float iFramesTime = 1.0f;
    public float iFramesTimer;
    public GameObject Camera;
    public GameObject GameManager;
    public GameObject Player1;
    public float PlayerOffset;
    public float SpeedUpTimer;
    public float SpeedUpTime = 5.0f;
    public float KnockbackIncreaseTimer;
    public float KnockbackIncreaseTime = 5.0f;
    public float SlowDownTimer;
    public float SlowDownTime = 5.0f;
    public bool canNotInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerOffset = transform.position.x;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(groundSpeed * Time.deltaTime, 0, 0); //Move Player (2) on X axis.
        PlayerOffset += groundSpeed * Time.deltaTime;
        transform.position = new Vector3(Camera.transform.position.x + PlayerOffset, transform.position.y, Camera.transform.position.z + 2); //Keep Player (1) on Z axis.
        canNotInput = Camera.GetComponent<CameraMovement>().actCleared;
    if (!canNotInput)
    {
        if (Input.GetKey(KeyCode.LeftArrow)) //Player (2) presses left.
        {
            if (groundSpeed > 0) //Deceleration.
            {
                groundSpeed -= deceleration;
                if (groundSpeed <= 0)
                    groundSpeed = 0;
            }
        else if (groundSpeed > -maxSpeed) //Acceleration.
            {
                groundSpeed -= acceleration;
                if (groundSpeed <= -maxSpeed)
                    groundSpeed = -maxSpeed;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)) //Player (2) presses right.
        {
            if (groundSpeed < 0) //Deceleration.
            {
                groundSpeed += deceleration;
                if (groundSpeed >= 0)
                    groundSpeed = 0;
            }
        else if (groundSpeed < maxSpeed) //Acceleration.
            {
                groundSpeed += acceleration;
                if (groundSpeed >= maxSpeed)
                    groundSpeed = maxSpeed;
            }
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) //Player (2) is not pressing left or right.
        {
             groundSpeed -= Mathf.Min(Mathf.Abs(groundSpeed), friction) * Mathf.Sign(groundSpeed); //Decellerate.
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)&& isGrounded()) //Jumping!
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            playerAudio.PlayOneShot(playerJump, 1.0f);
        }
    }
        if (transform.position.y < -3) //Player (2) falls off stage.
        {
            transform.position = new Vector3(Camera.transform.position.x, 1.5f, Camera.transform.position.z + 2);
            groundSpeed = 0;
            PlayerOffset = 0;
            GameManager.GetComponent<GameManager>().P2Coins -= GameManager.GetComponent<GameManager>().P2Coins;
        }
        SpeedUpTimer -= Time.deltaTime; //SpeedUp timer counts down.
        if ((SpeedUpTimer <= 0) && (SlowDownTimer <= 0 )) //SpeedUp wears off.
        {
        acceleration = 0.046875f;
        maxSpeed = 6.0f;
        SpeedUpTimer = 0;
        }
        SlowDownTimer -= Time.deltaTime; //SlowDown timer counts down.
        if ((SlowDownTimer <= 0) && (SpeedUpTimer <= 0) ) //SlowDown wears off.
        {
        acceleration = 0.046875f;
        maxSpeed = 6.0f;
        SlowDownTimer = 0;
        }
        iFramesTimer -= Time.deltaTime; //iFrames count down.
        if (iFramesTimer < 0) //iFrames wear off.
        {
            iFramesTimer = 0;
        }
        KnockbackIncreaseTimer -= Time.deltaTime; //Knockback increase timer counts down.
        if (KnockbackIncreaseTimer < 0) //Knockback increase wears off.
        {
        knockbackStrength = 1.0f;
        KnockbackIncreaseTimer = 0;
        }
    bool isGrounded() //Checks if Player (2) is grounded.
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.25f);
    }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P2Coins += 1;
            playerAudio.PlayOneShot(collectCoin, 1.0f);
            
        }
         if (other.gameObject.CompareTag("Hazard") && iFramesTimer <= 0)
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P1Coins /= 2;
            playerAudio.PlayOneShot(playerHurt, 1.0f);
            iFramesTimer = iFramesTime;
        }
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            SpeedUpTimer = SpeedUpTime;
            acceleration *= 2;
            maxSpeed = 12;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
        if (other.gameObject.CompareTag("SuperCoin"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P2Coins += 10;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
        if (other.gameObject.CompareTag("SpeedDown"))
        {
            Destroy(other.gameObject);
            SlowDownTimer = SlowDownTime;
            acceleration /= 2;
            maxSpeed = 3;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        if (other.gameObject.CompareTag("KnockbackUp"))
        {
            Destroy(other.gameObject);
            KnockbackIncreaseTimer = KnockbackIncreaseTime;
            knockbackStrength *=2;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        
        }
        }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player 1") && Mathf.Abs(groundSpeed) < Mathf.Abs(Player1.GetComponent<Player1Movement>().groundSpeed))
        {
            groundSpeed = Player1.GetComponent<Player1Movement>().groundSpeed * Player1.GetComponent<Player1Movement>().knockbackStrength * 2;
            Player1.GetComponent<Player1Movement>().groundSpeed = 0;
            playerAudio.PlayOneShot(playerHurt, 1.0f);
        }
        else if (other.gameObject.CompareTag("Player 1") && Mathf.Abs(groundSpeed) > Mathf.Abs(Player1.GetComponent<Player1Movement>().groundSpeed))
        {
            Player1.GetComponent<Player1Movement>().groundSpeed = groundSpeed * knockbackStrength * 2;
            groundSpeed = 0;
            playerAudio.PlayOneShot(playerHurt, 1.0f);
        }
}
}

