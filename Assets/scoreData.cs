using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreData : MonoBehaviour
{
    float timeCompleted = 0;
    public int emergencyTeleports = 0;
    public static bool escaped = false;

    public static float savedTime = 0;
    public static int savedTeleports = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCompleted += Time.deltaTime;

    }

    public void sendData(bool escape)
    {
        savedTime = timeCompleted;
        savedTeleports = emergencyTeleports;
        escaped = escape;
    }
}
