using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    Scene loadscene;
    // Start is called before the first frame update
    void Start()
    {
       loadscene=SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        //escで現在のシーンをロードし直し
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SceneManager.LoadScene(loadscene.name);
        }
    }
}
