using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private RawImage background;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        //Modify Vector2 to scroll y
        Vector2 backgroundOffset = new(0, scrollSpeed * Time.time);
        //Set x and y offset to background offset
        background.uvRect = new(backgroundOffset, new(1, 1));
    }
}
