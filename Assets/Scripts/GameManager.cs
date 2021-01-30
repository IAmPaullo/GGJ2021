using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    float currentTime;
    [SerializeField] int startingTime = 20;
    [SerializeField] Text timeText;

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }


    void UpdateTime()
    {
        currentTime -= 1 * Time.deltaTime;
        
       

        timeText.text = "Time: " + Mathf.RoundToInt(currentTime).ToString();
        

        if (currentTime <= 0)
            currentTime = 0;

    }







}
