using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
    public List<Connector> connectors;
    public float movingSpeed;
    public float startTime;
    public float endTime;
    public Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = new Vector3(transform.position.x + (movingSpeed * endTime), transform.position.y);
        endTime += 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
