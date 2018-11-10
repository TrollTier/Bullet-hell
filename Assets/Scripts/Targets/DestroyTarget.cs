using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyTarget : MonoBehaviour {

    public GameObject[] toDestroy;
    public string nextScene;
	
	// Update is called once per frame
	void LateUpdate () {
        int targetAlive = 0;

        for (int i = 0; i < toDestroy.Length; i++)
        {
            if (toDestroy[i] != null && toDestroy[i].activeSelf)
                targetAlive++;
        }

        if (targetAlive == 0)
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        
	}
}
