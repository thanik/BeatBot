using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Image black;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        black.DOFade(0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void transitionToOtherScene(int sceneIndex)
    {
        StartCoroutine(loadScene(sceneIndex));
    }

    IEnumerator loadScene(int sceneIndex)
    {
        black.DOFade(1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneIndex);
        while (!loadOp.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        black.DOFade(0f, 0.5f);
    }
}
