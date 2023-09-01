using System;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _chainSpeed = 2f;
    private AudioSource src;
    [SerializeField] private AudioClip ac;
    
    public static bool IsFired;

    private void Start()
    {
        src = GameObject.FindObjectOfType<AudioSource>();
        IsFired = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // For playing the shooting sound only once when the chain has been released
            if (!IsFired)
            {
                src.clip = ac;
                if (GameUI.Instance.IsAudioEnable())
                {
                    src.Play();
                }
            }
            IsFired = true;
        };
        if (IsFired)
        {
            transform.localScale += Vector3.up * (Time.deltaTime * _chainSpeed);
        }
        else
        {
            transform.position = _playerTransform.position;
            transform.localScale = new Vector3(1f, 0f, 1f);
        }
    }
}
