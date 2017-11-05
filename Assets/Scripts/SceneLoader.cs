﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public bool isOnLoadingScene = false;
	public GameObject[] characters;

	void Start ()
	{
		if (isOnLoadingScene) {
			// Load the next scene
			string sceneName = ApplicationController.ac.nextSceneToLoad;
			if (sceneName != "")
				//LoadScene (sceneName);
				StartCoroutine (LoadAsyncScene (sceneName));
			else
				LoadScene ("main_menu");

			// Activate one character to display
			GameObject character = characters [Random.Range (0, characters.Length)];
			character.SetActive (true);
			Debug.Log ("loading screen character = " + character.name);

			// Animate character
			AnimateCharacter (character);
		}
	}

	public void LoadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public static void LoadSceneWithLoadingScreen (string sceneName)
	{
		ApplicationController.ac.nextSceneToLoad = sceneName;
		SceneManager.LoadScene ("loading_scene");
	}


	IEnumerator LoadAsyncScene (string sceneName)
	{
		// The Application loads the Scene in the background at the same time as the current Scene.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

		//Wait until the last operation fully loads to return anything
		while (!asyncLoad.isDone) {
			Debug.Log ("loading : " + asyncLoad.progress);
			yield return null;
		}
	} 

	void AnimateCharacter (GameObject character)
	{
		switch (character.name) {
		case "Cat":
			character.GetComponent<Animator> ().SetFloat ("speed", 1f);
			break;
		case "Dog":
			character.GetComponent<Animator> ().SetFloat ("x.velocity", 1f);
			break;
		default:
			break;
		}
	}
}
