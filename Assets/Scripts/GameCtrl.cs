using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(loadscene.name);
        }
    }
}
