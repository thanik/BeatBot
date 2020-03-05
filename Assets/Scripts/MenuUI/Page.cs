using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    // Start is called before the first frame update
    public Selectable[] buttons;
    public int currentButtonIndex = 0;
    public Selectable currentButton;
    //public 
    void Start()
    {
        buttons = GetComponentsInChildren<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
