using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _interactionText;
    [SerializeField] private GameObject _mainCanvas;
    public GameObject player;
    [SerializeField] private GameObject _levelEraser;
    public Material levelErasedMat;

    public int currentLevel = 1;
    public const int lastLevelIndex = 2;
    
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
                GameOver(false);
            }
        }
    }
    
    private int _LevelScore = 0;
    public int LevelScore
    {
        get { return _LevelScore; }
        set
        {
            _LevelScore = value;
        }
    }
    
    public const float timeDecreasePerSecond = .01f;

    public static GameManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(_mainCanvas);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(_levelEraser);
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
        if (++currentLevel > lastLevelIndex)
        {
            GameOver(true);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevel);
        }
    }
    
    public void GameOver(bool success)
    {
        //TODO
    }
}
