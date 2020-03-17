using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scollingSpeed = 1f;
    public Vector3 resetPos;
    public Transform[] backgrounds;

    private float moveX;
    private float cameraY;
    private Camera mainCamera;
    GameController gmCtrl;
    // Start is called before the first frame update
    void Start()
    {
        backgrounds = GetComponentsInChildren<Transform>();
        mainCamera = Camera.main;
        gmCtrl = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (backgrounds.Length > 2 && gmCtrl.isPlaying && gmCtrl.gameTime > gmCtrl.timeToLockCamera)
        {
            moveX = (-Time.deltaTime * scollingSpeed);
            for (int i = 1; i < backgrounds.Length; i++)
            {
                Vector3 localPos = backgrounds[i].localPosition;
                cameraY = -mainCamera.transform.position.y;
                backgrounds[i].localPosition = new Vector3(localPos.x + moveX, cameraY, localPos.z);
                
                if (backgrounds[i].localPosition.x < -resetPos.x)
                {
                    //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
                    Vector2 groundOffSet = new Vector2(resetPos.x * 2f, 0);
                    //Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
                    backgrounds[i].localPosition = (Vector2)backgrounds[i].localPosition + groundOffSet;
                    //backgrounds[i].position = resetPos;
                }
            }
        }
    }
}
