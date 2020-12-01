using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacmanMove : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public bool wantsChange = false;
    public Vector3 directionTarget;
    public Transform[] rayPositions;
    public Vector3 rayDir;
    public float rayDirLength;
    public float posXMin;
    public float posXMax;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            changeDirection(transform.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            changeDirection(transform.forward * -1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            changeDirection(transform.right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            changeDirection(transform.right * -1);
        }
        if (wantsChange)
        {
            checkDirection();
        }
        checkFront();
        moveInDirection();
        checkSides();
    }
    public void changeDirection(Vector3 dir)
    {
        directionTarget = dir;
        rayDir = dir;
        wantsChange = true;
    }

    public void checkFront()
    {
        Vector3 newRayPos;
        newRayPos = transform.position + direction;
        if (Physics.Raycast(transform.position, direction, 1.6f))
        {
            direction = Vector3.zero;
        }
        Debug.DrawRay(transform.position, direction);
    }

    public void moveInDirection()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void checkSides()
    {
        if (direction == transform.right * -1 && transform.position.x < posXMin)
        {
            transform.position = new Vector3(posXMax, transform.position.y, transform.position.z);
        }

        if (direction == transform.right && transform.position.x > posXMax)
        {
            transform.position = new Vector3(posXMin, transform.position.y, transform.position.z);
        }

        //test
    }

    public void checkDirection()
    {
        LayerMask layerMask;
        layerMask = LayerMask.GetMask("Wall");
        Vector3 overlapBoxCenter = transform.position + directionTarget;
        Collider[] hitColliders = Physics.OverlapBox(overlapBoxCenter, new Vector3(rayDirLength, 0.2f, rayDirLength), Quaternion.identity, layerMask);

        if (hitColliders.Length == 0)
        {
            direction = directionTarget;
            wantsChange = false;
        }
        else
        {
            foreach (Collider col in hitColliders)
            {
                Debug.Log(col.name);
            }
        }
        
        /**int raysColliding = 0;
        foreach(Transform rayPos in rayPositions)
        {
            if (Physics.Raycast(rayPos.position, rayDir, rayDirLength))
            {
                raysColliding += 1;
            }
        }
        Debug.Log(raysColliding);
        if (raysColliding == 0)
        {
            direction = directionTarget;
            wantsChange = false;
        }
        **/
    }
}
