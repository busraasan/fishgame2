using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class finishscript : MonoBehaviour
{
    public TextMeshProUGUI finishedDisplay;
    public GameObject menuButton;
    
    void Start()
    {
        menuButton.SetActive(false);
    }  
    
    public void viewText(bool menuButtonActivation)
    {
        menuButton.SetActive(menuButtonActivation);
        finishedDisplay.text = "Finished!";
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
