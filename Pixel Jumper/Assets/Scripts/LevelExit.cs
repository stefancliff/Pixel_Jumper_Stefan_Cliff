using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
        
       
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(waitTime);
        
        int currentSceneIndex   = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex      = currentSceneIndex + 1;
        
        // This is our way of checking and avoiding an error for when the player gets to the end of the last level, to reset the index back to 0 (The first Level)
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings) 
        {
            nextSceneIndex = 0;
        }
        
        FindObjectOfType<ScenePersist>().ResetScenePersists();
        SceneManager.LoadScene(nextSceneIndex);
    }

   
}
