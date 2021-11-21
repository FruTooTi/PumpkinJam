using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _interactionText;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _failedPanel;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Text _failedPanelText;
    public GameObject player;
    public PlayerStartPoint levelStartPoint;
    [SerializeField] private GameObject _levelEraser;
    public Material levelErasedMat;

    public int currentLevel = 2;
    public const int lastLevelIndex = 8;
    
    private float _TimeLeft = 1f;
    public float TimeLeft
    {
        get { return _TimeLeft; }
        set
        {
            _timeSlider.value = value;
            _TimeLeft = value;
            if (value <= 0)
            {
                //GameOver(GameOverStatus.TimeOver);
            }
        }
    }

    public const float timeDecreasePerSecond = .02f;

    public static GameManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_mainCanvas);
            DontDestroyOnLoad(player);
            //DontDestroyOnLoad(_levelEraser);

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (arg0, mode) =>
            {
                if (currentLevel >= 2)
                {
                    levelStartPoint = GameObject.FindObjectOfType<PlayerStartPoint>();
                    levelStartPoint.PullPlayer();
                    //_levelEraser.transform.position = levelStartPoint.transform.position - new Vector3(20, 0, 0);
                    _failedPanel.SetActive(false);
                    PlayerMovement.Instance.isFailed = false;
                    PlayerMovement.Instance.movementEnabled = true;
                    if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    }
                }
            };

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        TimeLeft -= Time.deltaTime * timeDecreasePerSecond;
    }

    public void ShowInteractionMessage(IInteractable interactable)
    {
        SetActiveInteractionMessage(true);
        _interactionText.text = interactable.interactionMessage;
    }

    public void SetActiveInteractionMessage(bool state)
    {
        _interactionText.transform.parent.gameObject.SetActive(state);
    }

    public void LevelUp()
    {
        currentLevel++;
        if (currentLevel > lastLevelIndex)
        {
            GameOver(GameOverStatus.Finished);
        }
        else
        {
            TimeLeft = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevel);
        }
    }

    public void CloseFailPanel()
    {
        _failedPanel.SetActive(false);
    }

    public void RestartLevel()
    {
        _failedPanel.SetActive(false);
        PlayerMovement.Instance.movementEnabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(currentLevel);
    }
    
    public enum GameOverStatus
    {
        Finished,
        TimeOver,
        FellOff
    };
    public void GameOver(GameOverStatus status)
    {
        PlayerMovement.Instance.movementEnabled = false;
        if (status == GameOverStatus.Finished)
        {
            _endPanel.SetActive(true);
        }
        else
        {
            _failedPanel.SetActive(true);
            string failString = "";
            switch (status)
            {
                case GameOverStatus.FellOff:
                    failString = "No, you can't fly.";
                    break;
                case GameOverStatus.TimeOver:
                    failString = "Be faster next time.";
                    break;
            }

            _failedPanelText.text = "Failed: \n" + failString;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnMainMenu()
    {
        Destroy(_mainCanvas);
        Destroy(player);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
