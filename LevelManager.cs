LevelManager.cs:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
            public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string[] levels;
    private int currentLevelIndex = 0;
                private void Start()
    {
        LoadLevel(currentLevelIndex);
   }
     public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            Debug.LogError("Level index out of bounds!");
            return;
        }
                      SceneManager.LoadScene(levels[levelIndex]);
    }public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length)
        {
            currentLevelIndex = 0;
        }
        LoadLevel(currentLevelIndex);
    }public void ReloadCurrentLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void SetCurrentLevelIndex(int index)
    {
        currentLevelIndex = index;
    }
}
