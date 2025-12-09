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
}
