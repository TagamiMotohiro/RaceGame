using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoder : MonoBehaviour
{
	public static void LoadMainGame()
	{
		SceneManager.LoadScene("Track");
	}
	public static void LoadResult()
	{
		SceneManager.LoadScene("Result");
	}
}
