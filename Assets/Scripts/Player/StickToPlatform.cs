using UnityEngine;
using System.Collections;

public class StickToPlatform : MonoBehaviour , IPooledObj
{
    public float width = 2f; //Point where platform should change xDir
    static float initialSpeed = 0.05f;//0.05f;

    int xDirection = 1;

    static readonly Vector2 vRight = new Vector2(0.3f, 0);
    Vector3 pos;
    [SerializeField]
    Collider rCollider;
    [SerializeField]
    Collider lCollider;
    [SerializeField]
    Collider center;

    public Transform Goal;

    public bool spread = false;
    float t = 0;
    static readonly Vector3 newPos = new Vector3(5, 0, 0);
    static readonly Vector3 newRot = new Vector3(0, 3, 0);

    void Start()
    {
        //PlayerDirectionChange.platformPass += increaseSpeed;
    }

    void FixedUpdate()
    {
        pos = transform.position;
        if (pos.x >= width)
            xDirection = -1;
        else if (pos.x <= -width)
            xDirection = 1;

        if (spread)
            SpreadPlatforms();
        else
            StartCoroutine("MovePlatforms");
            //transform.Translate(vRight * xDirection * initialSpeed);
    }

    IEnumerator MovePlatforms()
    {
        transform.Translate(vRight * xDirection * initialSpeed);
        yield return null;
    }

    void SpreadPlatforms() //Diable colliders and spread platforms
    {
        rCollider.enabled = false;
        lCollider.enabled = false;

        rCollider.transform.localPosition += newPos * Time.deltaTime;
        lCollider.transform.localPosition += -newPos * Time.deltaTime;
        //rCollider.transform.localEulerAngles += newRot;
        //rCollider.transform.localEulerAngles -= newRot;
        StartCoroutine(ResetPlatforms());
    }

    IEnumerator ResetPlatforms()
    {
        yield return new WaitForSeconds(2);

        spread = false; //Stop moving goal platform

        rCollider.enabled = true; //Enabling Colliders Back
        lCollider.enabled = true;

        rCollider.transform.localPosition = new Vector3(3f, 0, 0); //Reset platform rotation and position
        lCollider.transform.localPosition = new Vector3(-3f, 0, 0);
        rCollider.transform.localEulerAngles = new Vector3(0, 0, 0);
        rCollider.transform.localEulerAngles = new Vector3(0, 0, 0);

        Goal.gameObject.SetActive(true); //Enable goal after delay to prevent double points gain
    }

    public static void increaseSpeed()
    {
        if(initialSpeed <= 0.15f)
            initialSpeed += initialSpeed/50;        //Max : 0.2f   Min : 0.15f
    }

    public void OnObjectSpawn()
    {
        ChangePlatformSize(Random.Range(0f,1f));
    }

    void ChangePlatformSize(float change)
    {
        width = 2f + change * 0.55f;

        center.transform.localScale    = new Vector3(2 - change,0.1f,1);
        lCollider.transform.localScale = new Vector3(4 + change,0.1f,1);
        rCollider.transform.localScale = new Vector3(4 + change,0.1f,1);
    }

    // IEnumerator DelaySpread()
    // {
    //     yield return new WaitForFixedUpdate();
    //     yield return new WaitForFixedUpdate();
    //     SpreadPlatforms();
    // }
}
