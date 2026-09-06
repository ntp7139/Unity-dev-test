using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class RegisterOnload : MonoBehaviour
{
    [Header("Balls and Goals")]
    public List<GoalObject> goalObjects;
    public List<BallController> balls;

    [Header("Button")]
    public GameObject hideNormalButton;
    public GameObject panelEndgame;
    public List<GameObject> buttons;

    void Start()
    {
        RegisterOnLoad();
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void RegisterGameManager()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterOnLoad(goalObjects, balls);
        }
    }

    void RegisterUIManager()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.RegisterOnLoad(buttons, hideNormalButton, panelEndgame);
        }
    }

    void RegisterOnLoad()
    {
        RegisterGameManager();
        RegisterUIManager();
    }
}
