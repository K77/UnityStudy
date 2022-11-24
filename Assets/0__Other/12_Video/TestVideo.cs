using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TestVideo : MonoBehaviour
{
private VideoPlayer videoPlayer;
    private RawImage rawImage;

    void Start()
    {
        videoPlayer = this.GetComponent<VideoPlayer>();
        rawImage = this.GetComponent<RawImage>();
    }

    void Update()
    {
        if (videoPlayer.texture == null)
        {
            return;
        }
        rawImage.texture = videoPlayer.texture;
    }

}
