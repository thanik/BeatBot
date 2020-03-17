﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSelector : MonoBehaviour
{
    public Page parentPage;
    public AudioSource backgroundMusic;
    public float yOffset;
    public float jumpPower;
    // Start is called before the first frame update
    void Start()
    {
        parentPage = GetComponentInParent<Page>();
        backgroundMusic.DOFade(1f, 0.25f);
        //currentButton = parentPage.buttons[currentButtonIndex];
        //Vector3 currentButtonPos = currentButton.transform.position;
        //transform.position = new Vector3(currentButtonPos.x, currentButtonPos.y + 1.5f, currentButtonPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (parentPage.currentButtonIndex > 0)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex - 1];
                Vector3 destinationPos = new Vector3(nextButton.transform.position.x, nextButton.transform.position.y + yOffset, nextButton.transform.position.z);
                transform.DOJump(destinationPos, jumpPower, 1, 0.25f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
                parentPage.currentButtonIndex--;
                parentPage.currentButton = nextButton;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (parentPage.currentButtonIndex < parentPage.buttons.Length - 1)
            {
                Selectable nextButton = parentPage.buttons[parentPage.currentButtonIndex + 1];
                Vector3 destinationPos = new Vector3(nextButton.transform.position.x, nextButton.transform.position.y + yOffset, nextButton.transform.position.z);
                transform.DOJump(destinationPos, jumpPower, 1, 0.25f);
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
                    break;
                case "new_game":
                    backgroundMusic.DOFade(0f, 0.25f);
                    FindObjectOfType<SceneLoader>().transitionToOtherScene(1);
                    break;
                case "music":
                    break;
                case "quit":
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
}
