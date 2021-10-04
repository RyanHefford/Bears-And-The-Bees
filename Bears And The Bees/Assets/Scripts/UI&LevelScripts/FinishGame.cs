using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetInt("SuccessfulRuns").Equals(default))
            {
                PlayerPrefs.SetInt("SuccessfulRuns", 0);
            }
            else
            {
                int newRun = PlayerPrefs.GetInt("SuccessfulRuns") + 1;
                PlayerPrefs.SetInt("SuccessfulRuns", newRun);
            }

            SceneManager.LoadScene("EndGameScene");
        }
    }
}
