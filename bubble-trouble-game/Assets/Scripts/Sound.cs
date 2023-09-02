using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public List<Sprite> spriteList;

    private bool _IsEnable = true;
    private int _curSpriteID = 0;
    private Image _imageComp;
    private GameObject _imageCompGameObject;

    public static Sound Instance { get; private set; }

    void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ChangeImage()
    {
        _imageCompGameObject = GameObject.FindWithTag("Sound");

        if (_imageCompGameObject != null)
            _imageComp = _imageCompGameObject.GetComponent<Image>();

        _curSpriteID++;
        if (_curSpriteID == spriteList.Count)
        {
            Debug.Log("Sound unmute");
            _curSpriteID = 0;
            _IsEnable = true;
        }
        else
        {
            Debug.Log("Sound mute");
            _IsEnable = false;
        }
        _imageComp.sprite = spriteList[_curSpriteID];
    }

    public bool IsAudioEnable()
    {
        return _IsEnable;
    }
}
