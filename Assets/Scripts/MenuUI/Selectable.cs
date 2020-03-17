using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GetComponent<Image>().sprite = selected;
        }
       else
        {
            GetComponent<Image>().sprite = unselected;
        }
    }
}
