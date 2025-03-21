using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] private string videoUrl = "https://www.youtube.com/watch?v=y-lr1loC5Js";
    private VideoPlayer videoplayer;
    public void StartVideo() {
        videoplayer = GetComponent<VideoPlayer>();
        if(videoplayer) {
            videoplayer.url = videoUrl;
            videoplayer.playOnAwake = false;
            videoplayer.Prepare();

            videoplayer.prepareCompleted += OnVideoPrepared;
        }
    }
    private void OnVideoPrepared(VideoPlayer source) {
        videoplayer.Play();
    }

    //Add a gameobject with the videoplater property to the gamescene and add vid player script adn add a rendertexture add it on a canvas and add a rawimage set it to the same dimensions as render texture
}
