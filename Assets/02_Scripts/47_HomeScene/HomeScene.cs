using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    public void LoadStageScene()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void LoadCharacterScene()
    {
        SceneManager.LoadScene("CharacterScene");
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
