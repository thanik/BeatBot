using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public Sprite unselected;
    public Sprite selected;
    public Page page;
    // Start is called before the first frame update
    void Start()
    {
        page = GetComponentInParent<Page>();
    }

    // Update is called once per frame
    void Update()
    {
       if (page.currentButton == this)
        {
            GetComponent<SpriteRenderer>().sprite = selected;
        }
       else
        {
            GetComponent<SpriteRenderer>().sprite = unselected;
        }
    }
}
