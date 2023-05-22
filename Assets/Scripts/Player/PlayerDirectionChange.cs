using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class PlayerDirectionChange : MonoBehaviour
{
    public float jumpForce = 7f; //Jump height
    public Transform parent;     //Canvas
    public static Rigidbody rb;

    InputControll InputControll;    //Singleton
    ObjectPooler objectPooler;      //Singleton
    AudioManager audioManager;      //Singleton
    HighScoreManager scoreManager;  //Singleton

    Vector3 velocityVec;
    int direction = 2;               //Plater X-axis angle 
    int multiplier = 0;              // Score Combo Multiplier
    int scoreComboResult;
    static int platformCount;
    string poolTag = "PopUpText";
    bool wallBounce = true;
    bool bounce = true;

    RaycastHit hit;
    public LayerMask mask;
    public float rayDistance = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = AudioManager.instance;     // SoundManager Instace
        InputControll = InputControll.instance;   // Player Input Instace
        objectPooler = ObjectPooler.Instance;     // Pool Of Objects
        scoreManager = HighScoreManager.instance; // Pool Of Objects

        platformCount = 0;
    }

    void Update()
    {
        if (!MenuController.GameIsPaused && InputControll.Tap)
            direction *= -1;

        if (parent == null)
            parent = GameObject.FindGameObjectWithTag("Canvas").transform;
    }

    void FixedUpdate()
    {
        if (rb.velocity.y <= -12)
        {
            rb.velocity = new Vector3(rb.velocity.x, -12, rb.velocity.z);
        }

        if (rb.velocity.y <= 1)
        {
            Physics.Raycast(transform.position, -Vector3.up, out hit, rayDistance, mask);
            GoalHitLogic();
        }
    }

    void GoalHitLogic()
    {
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("CenterPlatform"))
            {
                if (hit.distance < 0.6f)
                {
                    hit.collider.GetComponentInParent<StickToPlatform>().spread = true;
                    bounce = false;
                }
            }
            else if (hit.collider.CompareTag("BouncePlatforms"))
            {
                bounce = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (bounce && other.gameObject.CompareTag("BouncePlatforms"))
            Bounce();
        else if (wallBounce && (other.gameObject.CompareTag("LeftWall") || other.gameObject.CompareTag("RightWall")))
            BounceWall();
    }

    void OnTriggerEnter(Collider other)
    {
        //Score Logic Goes Here For some Reason ¯\_(O_O)_/¯
        if (other.gameObject.CompareTag("GoalPlatform"))
        {
            countPlatforms();
            ScoreMultiplicationController();
            StickToPlatform.increaseSpeed(); // Each platfor increase their speed by X
            audioManager.PlaySound("Ding"); //Sound
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GoalPlatform"))
        {
            bounce = true;
            other.gameObject.SetActive(false);
        }
    }

    void Bounce()
    {
        velocityVec = rb.velocity;
        velocityVec.y = jumpForce;
        velocityVec.x = direction;
        rb.velocity = velocityVec;
        scoreManager.deadScore += 1;
        wallBounce = true;
        audioManager.PlaySound("Pop"); //Sound
    }

    void BounceWall()
    {
        velocityVec = rb.velocity;
        velocityVec.y = jumpForce / 2;
        velocityVec.x = direction;
        rb.velocity = velocityVec;
        wallBounce = false;
        audioManager.PlaySound("Pop"); //Sound
    }

    void ScoreMultiplicationController()
    {
        switch (scoreManager.deadScore)
        {
            case 0:
                multiplier = 5;
                break;
            case 1:
                multiplier = 3;
                break;
            case 2:
                multiplier = 2;
                break;
            case 3:
            default:
                multiplier = 1;
                break;
        }

        scoreComboResult = multiplier;

        objectPooler.spawnFromPoolText(poolTag, gameObject.transform, parent, scoreComboResult);
        scoreManager.target += scoreComboResult;
        scoreManager.deadScore = 0;
    }

    void countPlatforms()
    {
        platformCount++;
    }

    public static void StartOver()
    {
        platformCount = 0;
    }

}