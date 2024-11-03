using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionBar : MonoBehaviour
{
    [SerializeField] private Slider detectionBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateDetectionBar(float detectionLevelPercentage)    
    {
        detectionBar.value = detectionLevelPercentage;
    }
    public void hi() { print("Hhhhhhhhh"); }
}
