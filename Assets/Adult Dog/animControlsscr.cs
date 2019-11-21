using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class animControlsscr : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;

    [Range(-1, 5)]
    public float speed = 5;

    float maxSpeed = 5;
    
    //int _speed = 0;
    //bool _sit = false;
    bool moving = true;
    float distanceTravelled;

    Animator animator;
    // Start is called before the first frame update
    Vector3 prevPos = Vector3.zero;
    Quaternion newRot = Quaternion.identity;

    Vector3 newPos;
    
    //public float speed = 0.0f;

    [Range(1, 100)]
    public float transformSpeed = 1f;

    public float radiusPos = 5.0f;

    [Range(0, 1)]
    public float rotationSpeed = 0.1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // StartCoroutine("RandomPosition");
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }


    }
    public float moveTime = 0;
    // Update is called once per frame
    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
        animator.SetFloat("speed", speed / maxSpeed);
    }
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    /*
    IEnumerator RandomPosition()
    {
        while (transformSpeed > 0) {

            Vector2 np = Random.insideUnitCircle * radiusPos;
            prevPos = transform.position;
            moveTime = 0;
            newPos = new Vector3(np.x, transform.position.y, np.y); //transform.position + 

            Vector2 tst = new Vector2(prevPos.x - newPos.x, prevPos.z - newPos.z);
            newRot = Quaternion.Euler(new Vector3(0, (Mathf.Atan2(tst.x, tst.y) * Mathf.Rad2Deg) + 180, 0));
            
            //Debug.Log(); 
            yield return new WaitForSeconds(2.0f);
        }
    }
    */
}
