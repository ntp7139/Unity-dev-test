using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, ICanListenEvent
{
    public static UIManager Instance;

    [SerializeField]
    GameObject hideNormalKickButton;

    [SerializeField]
    GameObject panelEndGame;

    [SerializeField]
    List<GameObject> buttons;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }

    public void RegisterOnLoad(
        List<GameObject> buttons,
        GameObject hideNormalKickButton,
        GameObject panelEndGame
    )
    {
        this.hideNormalKickButton = hideNormalKickButton;
        this.panelEndGame = panelEndGame;
        this.buttons = buttons;
    }

    void Start()
    {
        this.Listen<AvailableToKickEvent>(OnAvailableToClick);
        this.Listen<NotAvailableToKickEvent>(OnNotAvailableToClick);
        this.Listen<OnCompleteGameEvent>(OnCompleteTGame);
        this.Listen<BallKickEvent>(OnBallKick);
        this.Listen<OnCongratsCompleteEvent>(OnCongratsComplete);
    }

    void OnAvailableToClick(AvailableToKickEvent evt)
    {
        if (hideNormalKickButton != null)
        {
            hideNormalKickButton.gameObject.SetActive(false);
        }
    }

    void OnNotAvailableToClick(NotAvailableToKickEvent evt)
    {
        if (hideNormalKickButton != null)
        {
            hideNormalKickButton.gameObject.SetActive(true);
        }
    }

    void OnCompleteTGame(OnCompleteGameEvent evt)
    {
        if (panelEndGame != null)
        {
            panelEndGame.gameObject.SetActive(true);
        }
    }

    void OnBallKick(BallKickEvent evt)
    {
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }
    }

    void OnCongratsComplete(OnCongratsCompleteEvent evt)
    {
        foreach (GameObject button in buttons)
        {
            if (button != null)
            {
                button.SetActive(true);
            }
        }
    }
}
