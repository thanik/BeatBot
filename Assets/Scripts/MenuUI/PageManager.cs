using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public Page[] pages;
    // Start is called before the first frame update
    void Start()
    {
        pages = GetComponentsInChildren<Page>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
