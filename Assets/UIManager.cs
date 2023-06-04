using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject InstructionsPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        Debug.Log("GameEntered");
        SceneManager.LoadScene("level1");
    }

    public void goToInstructions()
    {
        InstructionsPage.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }

    public void returnToeMenuFromInstructions()
    {
        InstructionsPage.SetActive(false);
    }
}
