using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class finalScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scores;
    [SerializeField] TextMeshProUGUI grade;

    List<string> gradeArray = new List<string> { "D","C","B","A","A*","S"};

    int gradeIndex = 0;

    float time;
    float teleports;

    [SerializeField] AudioSource explosion;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreData.escaped == false) { explosion.Play(); }
        time = scoreData.savedTime;
        teleports = scoreData.savedTeleports;
        scores.text = (time.ToString() + " Seconds\n\n"+teleports.ToString() + "\n\n"+scoreData.escaped.ToString());

        if (time < 300) { gradeIndex += 2; }
        else if (time < 360) { gradeIndex += 1; }
        else if (time < 540) { gradeIndex -= 1; }
        
        if (teleports == 0) { gradeIndex += 2; }
        else if (teleports < 6) { gradeIndex += 1; }
        else if (teleports > 15) { gradeIndex -= 1; }

        if (scoreData.escaped) { gradeIndex += 2; }
        else { gradeIndex -= 1; }

        if (gradeIndex < 0) { grade.text = ("Grade: F"); }
        else { grade.text = ("Grade: " + gradeArray[gradeIndex]); }

        scoreData.savedTime = 0;
        scoreData.savedTeleports = 0;
        scoreData.escaped = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
