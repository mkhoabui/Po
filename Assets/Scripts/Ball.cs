using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0f;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    //Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    CircleCollider2D myCircleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myCircleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }
    private void LaunchOnMouseClick()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    int count = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {   
            count++;
            if (count == 5)
            {
                LockBallToPaddle();
                hasStarted = false;
            }

            if (collision.gameObject.name == "Paddle" || collision.gameObject.name == "Prisoner")
            {
                myAudioSource.PlayOneShot(ballSounds[1]);
                count = 0;
            }
            else if (collision.gameObject.name == "Lose Collider")
            {
                myAudioSource.PlayOneShot(ballSounds[2]);
            }
            else
            {
                myAudioSource.PlayOneShot(ballSounds[0]);
            }
        }
    }
}
