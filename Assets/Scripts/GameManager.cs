using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Text _interactionText;
    
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
    
    public const float timeDecreasePerSecond = .01f;

    public static GameManager Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
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
        _interactionText.gameObject.SetActive(state);
    }
    
    public void GameOver(bool success)
    {
        //TODO
    }
}
