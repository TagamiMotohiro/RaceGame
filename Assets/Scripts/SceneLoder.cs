using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoder : MonoBehaviour
{
	public void Load_MainGame()
	{
		SceneManager.LoadScene("Track");
	}
	public void Load_Ranking()
	{
		SceneManager.LoadScene("Result");
	}
}
