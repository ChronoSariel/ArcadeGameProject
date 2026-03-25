using UnityEngine;

public class Player1Movement : MonoBehaviour
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
    public float PlayerOffset;
    public float SpeedUpTimer;
    public float SpeedUpTime = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerOffset = transform.position.x;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(groundSpeed * Time.deltaTime, 0, 0); //Move Player (1) on X axis.
        PlayerOffset += groundSpeed * Time.deltaTime;
        transform.position = new Vector3(Camera.transform.position.x + PlayerOffset, transform.position.y, Camera.transform.position.z + 2); //Keep Player (1) on Z axis.
        if (Input.GetKey(KeyCode.A)) //Player (1) presses left.
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
        if (Input.GetKey(KeyCode.D)) //Player (1) presses right.
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
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) //Player (1) is not pressing left or right.
        {
             groundSpeed -= Mathf.Min(Mathf.Abs(groundSpeed), friction) * Mathf.Sign(groundSpeed); //Decellerate.
        }
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded()) //Jumping!
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            playerAudio.PlayOneShot(playerJump, 1.0f);
        }
        if (transform.position.y < -3) //Player (1) falls off stage.
        {
            transform.position = new Vector3(Camera.transform.position.x, 1.5f, Camera.transform.position.z + 2);
            groundSpeed = 0;
            PlayerOffset = 0;
            GameManager.GetComponent<GameManager>().P1Coins -= GameManager.GetComponent<GameManager>().P1Coins;
        }
        SpeedUpTimer -= Time.deltaTime; //SpeedUp timer counts down.
        if (SpeedUpTimer <= 0) //SpeedUp wears off.
        {
        acceleration = 0.046875f;
        maxSpeed = 6.0f;
        SpeedUpTimer = 0;
        }
    }
    bool isGrounded() //Checks if Player (1) is grounded.
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.25f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P1Coins += 1;
            playerAudio.PlayOneShot(collectCoin, 1.0f);
            
        }
         if (other.gameObject.CompareTag("Hazard"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P1Coins /= 2;
            playerAudio.PlayOneShot(playerHurt, 1.0f);
        }
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            SpeedUpTimer = SpeedUpTime;
            acceleration *= 2;
            maxSpeed *= 2;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
        if (other.gameObject.CompareTag("SuperCoin"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().P1Coins += 10;
            playerAudio.PlayOneShot(collectPowerUp, 1.0f);
        }
    }
}
