using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public bool timerActive = false;
    float escapeTimeMins = 2;
    float escapeTimeSeconds = 30;
    float escapeTimeMilSecs = 0;

    TextMeshProUGUI objectives;
    [SerializeField]GameObject objectivesObject;
    [SerializeField]scoreData scoreData;
    // Start is called before the first frame update
    void Start()
    {
        objectives = objectivesObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            escapeTimeMilSecs -= Time.deltaTime*1000;
            if (escapeTimeMilSecs < 0)
            {
                escapeTimeMilSecs = 1000;
                escapeTimeSeconds -= 1;
                if (escapeTimeSeconds < 0)
                {
                    escapeTimeMins -= 1;
                    if (escapeTimeMins < 0)
                        {
                            scoreData.sendData(false);
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                    escapeTimeSeconds = 60;
                }
            }
            objectives.text = ("Self Destruct: "+escapeTimeMins.ToString() + ":" + escapeTimeSeconds.ToString()+":" + ((int)escapeTimeMilSecs).ToString()+"\nEscape (optional)");
            
        }
    }
}
