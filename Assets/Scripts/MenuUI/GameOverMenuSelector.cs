using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuSelector : MonoBehaviour
{
    public Page parentPage;
    // Start is called before the first frame update
    void Start()
    {
        parentPage = GetComponentInParent<Page>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (parentPage.currentButtonIndex > 0)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex - 1];
                parentPage.currentButtonIndex--;
                parentPage.currentButton = nextButton;
            }

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (parentPage.currentButtonIndex < parentPage.buttons.Length - 1)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex + 1];
                parentPage.currentButtonIndex++;
                parentPage.currentButton = nextButton;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
        {
            switch (parentPage.currentButton.name)
            {
                case "restart":
                    FindObjectOfType<GameController>().resetLevel();
                    FindObjectOfType<SceneLoader>().transitionToOtherScene(1);
                    break;
                case "quit":
                    FindObjectOfType<SceneLoader>().transitionToOtherScene(0);
                    break;
                default:
                    break;
            }
        }
    }
}
