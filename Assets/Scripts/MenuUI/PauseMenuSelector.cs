using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSelector : MonoBehaviour
{
    public Page parentPage;
    GameController gmCtrl;
    // Start is called before the first frame update
    void Start()
    {
        parentPage = GetComponentInParent<Page>();
        gmCtrl = FindObjectOfType<GameController>();
        //currentButton = parentPage.buttons[currentButtonIndex];
        //Vector3 currentButtonPos = currentButton.transform.position;
        //transform.position = new Vector3(currentButtonPos.x, currentButtonPos.y + 1.5f, currentButtonPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (parentPage.currentButtonIndex > 0)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex - 1];
                Vector3 destinationPos = new Vector3(nextButton.transform.position.x, nextButton.transform.position.y + 80f, nextButton.transform.position.z);
                transform.DOJump(destinationPos, 40f, 1, 0.25f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                parentPage.currentButtonIndex--;
                parentPage.currentButton = nextButton;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (parentPage.currentButtonIndex < parentPage.buttons.Length - 1)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex + 1];
                Vector3 destinationPos = new Vector3(nextButton.transform.position.x, nextButton.transform.position.y + 80f, nextButton.transform.position.z);
                transform.DOJump(destinationPos, 40f, 1, 0.25f);
                transform.localScale = new Vector3(1f, 1f, 1f);
                parentPage.currentButtonIndex++;
                parentPage.currentButton = nextButton;
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
        {
            switch(parentPage.currentButton.name)
            {
                case "continue":
                    gmCtrl.togglePause();
                    break;
                case "restart":
                    parentPage.gameObject.SetActive(false);
                    FindObjectOfType<GameController>().resetLevel();
                    FindObjectOfType<SceneLoader>().transitionToOtherScene(1);
                    break;
                case "quit":
                    parentPage.gameObject.SetActive(false);
                    FindObjectOfType<SceneLoader>().transitionToOtherScene(0);
                    break;
                default:
                    break;
            }
        }
    }
}
