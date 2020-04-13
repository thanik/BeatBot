using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public List<Connector> connectors;
    //public float movingSpeed;
    public float startTime;
    public float endTime;
    public Vector3 endPosition;

    public PlatformTypeEnum platformType;
    public PlatformSizeEnum platformSize;
    // Start is called before the first frame update
    void Start()
    {
        //endPosition = new Vector3(transform.position.x + (movingSpeed * (endTime - startTime)), transform.position.y);
        //endTime += 0.15f;
        if (startTime == 0f && endTime == 0f)
        {
            Debug.LogWarning("Rail " + gameObject.name + " start and end time haven't set yet.");
            gameObject.SetActive(false);
        }

        if (endPosition == Vector3.zero)
        {
            Debug.LogWarning("Rail " + gameObject.name + " end position haven't set yet.");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
