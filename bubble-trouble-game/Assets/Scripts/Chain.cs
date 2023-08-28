using System;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _chainSpeed = 2f;
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip ac;
    
    public static bool isFired;

    private void Start()
    {
        isFired = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isFired = true;
            src.clip = ac;
            src.Play();
        };
        if (isFired)
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
