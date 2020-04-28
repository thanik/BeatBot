using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private Rail thisRail;
    private float lastCalculatedLength = 0;
    private PlatformTypeEnum lastPlatformType;
    private PlatformSizeEnum lastPlatformSize;
    private float length;

    public GameObject platformPrefab;
    public Sprite[] sprites;
    public Sprite[] solidSprites;

    public Sprite[] underwaterSprites;
    public Sprite[] underwaterSolidSprites;

    private Sprite[] selectedSprites;
    private Sprite[] selectedSolidSprites;
    public List<SpriteRenderer> platforms;
    void Start()
    {
        foreach (SpriteRenderer platform in platforms)
        {
            Destroy(platform.gameObject);
        }
        thisRail = GetComponentInParent<Rail>();
        //length = thisRail.movingSpeed * (thisRail.endTime - thisRail.startTime);

        if (thisRail.platformType == PlatformTypeEnum.DESERT)
        {
            selectedSprites = sprites;
            selectedSolidSprites = solidSprites;
        }
        else if(thisRail.platformType == PlatformTypeEnum.UNDERWATER)
        {
            selectedSprites = underwaterSprites;
            selectedSolidSprites = underwaterSolidSprites;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //length = thisRail.movingSpeed * (thisRail.endTime - thisRail.startTime);
        length = thisRail.endPosition.x - thisRail.transform.position.x;
        if (lastCalculatedLength != length || lastPlatformType != thisRail.platformType)
        {
            
            GameObject firstPlatform = Instantiate(platformPrefab, transform);
            firstPlatform.GetComponent<SpriteRenderer>().sprite = selectedSprites[0];
            firstPlatform.transform.localPosition = new Vector3(selectedSprites[0].bounds.size.x / 2, 0f, 0f);
            platforms.Add(firstPlatform.GetComponent<SpriteRenderer>());

            float lastLength = platforms[0].bounds.size.x;

            if (lastLength >= length)
            {
                int randomIndex = Random.Range(0, selectedSolidSprites.Length);
                firstPlatform.GetComponent<SpriteRenderer>().sprite = selectedSolidSprites[randomIndex];
                firstPlatform.transform.localPosition = new Vector3(0.75f, 0f, 0f);
            }
            else
            {
                while (lastLength < length)
                {
                    int randomIndex = Random.Range(2, selectedSprites.Length);
                    if (sprites[randomIndex].bounds.size.x + lastLength > length)
                    {
                        GameObject nextPlatform = Instantiate(platformPrefab, transform);
                        nextPlatform.GetComponent<SpriteRenderer>().sprite = selectedSprites[1];
                        nextPlatform.transform.localPosition = new Vector3(lastLength + (selectedSprites[1].bounds.size.x / 2), 0f, 0f);
                        platforms.Add(nextPlatform.GetComponent<SpriteRenderer>());
                    }
                    else
                    {
                        GameObject nextPlatform = Instantiate(platformPrefab, transform);
                        nextPlatform.GetComponent<SpriteRenderer>().sprite = selectedSprites[randomIndex];
                        nextPlatform.transform.localPosition = new Vector3(lastLength + (selectedSprites[randomIndex].bounds.size.x / 2), 0f, 0f);
                        platforms.Add(nextPlatform.GetComponent<SpriteRenderer>());
                    }
                    lastLength += selectedSprites[randomIndex].bounds.size.x;

                }
            }

            lastCalculatedLength = length;
            lastPlatformType = thisRail.platformType;
        }

    }
}
