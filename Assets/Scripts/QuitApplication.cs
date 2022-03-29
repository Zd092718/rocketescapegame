using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        QuitApp();
    }

    void QuitApp()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            print("Escape Key Clicked to quit application");
        }
    }
}
