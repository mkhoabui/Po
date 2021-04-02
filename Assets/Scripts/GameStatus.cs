using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] bool isAutoplayEnabled = false;

    TimeFlowManager theTimeFlowManager;

    private void Start()
    {
        theTimeFlowManager = FindObjectOfType<TimeFlowManager>() ;
    }

    // Update is called once per frame
    void Update()
    {
        if (theTimeFlowManager.IsTimeSlowed() == false)
            Time.timeScale = gameSpeed;
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoplayEnabled;
    }
}
