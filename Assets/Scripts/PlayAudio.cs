using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudio : MonoBehaviour
{
    GameObject unitychan; //Unityちゃんそのものが入る変数
    void Start()
    {
        unitychan = GameObject.Find ("LipSyncSound"); //Unityちゃんをオブジェクトの名前から取得して変数に格納する
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void playAudio() {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioSource unitychanAudioSource = unitychan.GetComponent<AudioSource>();
        unitychanAudioSource.clip = audioSource.clip;
        // Debug.Log(audioSource);
        unitychan.GetComponent<AudioSource>().Play();
        // int unitychanHP = script.AudioSource; //新しく変数を宣言してその中にUnityChanScriptの変数HPを代入する
    }
}
