using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float acceleration = 0.046875f;
    public float deceleration = 0.5f;
    public float friction = 0.046875f;
    public float maxSpeed = 6.0f;
    public float groundSpeed;
    public float jumpHeight = 6.5f;
    public float knockbackStrength = 6.0f;
    public float iFramesTime = 1.0f;
    public float iFramesTimer;
    public GameObject Camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(groundSpeed * Time.deltaTime, 0, 0); //Move Player (1) on X axis.
        transform.position = new Vector3(transform.position.x, transform.position.y, Camera.transform.position.z + 2); //Keep Player (1) on Z axis.
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
    }
}
