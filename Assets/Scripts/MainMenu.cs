using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;
    public Color[] colors;
    public Image background;
    void Start()
    {
        StartCoroutine(ChangeBgColor());
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
    public void SetHowToPlayActive(bool active)
    {
        howToPlayPanel.SetActive(active);
        if (active)
        {
            creditsPanel.SetActive(false);
        }
    }
    
    public void SetCreditsActive(bool active)
    {
        creditsPanel.SetActive(active);
        if (active)
        {
            howToPlayPanel.SetActive(false);
        }
    }

    public IEnumerator ChangeBgColor()
    {
        int currentColorIndex = 0;
        Color currentColor = colors[0];
        while (currentColorIndex < colors.Length - 1)
        {
            currentColor = Color.Lerp(currentColor, colors[currentColorIndex + 1], Time.deltaTime * 10);
            yield return new WaitForSeconds(Time.deltaTime);
            background.color = currentColor;
            if (currentColor == colors[currentColorIndex + 1])
            {
                currentColorIndex++;
            }
        }

        StartCoroutine(ChangeBgColor());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
