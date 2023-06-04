using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GemsCollector healthData;
    public GameObject ResumeButton;
    public GameObject BlueGem;
    public GameObject RedGem;
    public GameObject GameOverPrompt, GameCompletedPrompt;
    //public GameObject BlueHealthText;
    //public GameObject RedHealthText;
    public TextMeshProUGUI BlueHealthText;
    public TextMeshProUGUI RedHealthText;
    public int enemiesKilled = 0;
    public TextMeshProUGUI EnemyKillCount;
    public TextMeshProUGUI RedGemsCollected;
    public TextMeshProUGUI BlueGemsCollected;
    public TextMeshProUGUI TimeElapsed, score;
    public TextMeshProUGUI CurrentScore;
    public GameObject EnemySpawn1, EnemySpawn2;
    public GameObject RedWarining, MapView, InstructionsPage;
    public Animator animator;
    bool isInstatiated = true;
    public int isNotDead = 1;
    AudioSource deathCry;
    float timeElapsed = 0;
    int playerScore;
    // Start is called before the first frame update
    void Start()
    {
       
        Time.timeScale = 1;
        for (int i=0; i<30; i++)
        {
            int x1 = Random.Range(-29 , 29 );
            int y1 = Random.Range(-29 , 29);
            x1 = (x1 / 3) *3;
            y1 = (y1 / 3) * 3;

            var position1 = new Vector3(x1, 1, y1);
          
            Instantiate(BlueGem, position1, Quaternion.identity);
          
        }


        for (int i=0; i<40; i++)
        {
            int x2 = Random.Range(-29, 29);
            int y2 = Random.Range(-29, 29);
            x2 = (x2 / 3) * 3;
            y2 = (y2 / 3) * 3;
            var position2 = new Vector3(x2, 1, y2);
            Instantiate(RedGem, position2, Quaternion.identity);
        }
        deathCry = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log("RedHealth : " + healthData.RedHealth + "BlueHealth : " + healthData.BlueHealth);
        if (healthData.RedHealth<=0 || healthData.BlueHealth<=0)
        {
            isNotDead = 0;
            animator.SetTrigger("Death");
            //deathCry.Play();
            StartCoroutine(GameOverDelay());
           
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            pauseGame();
        }
        if (enemiesKilled == 5 && isInstatiated)
        {
            isInstatiated = false;
            EnemySpawn1.SetActive(true);
            EnemySpawn2.SetActive(true);
        }


        if (healthData.RedHealth<=60)
        {
            RedWarining.SetActive(true);
        }
        else
        {
            RedWarining.SetActive(false);
        }

        if (healthData.RedCollection>=60 && healthData.BlueCollection>=40 && enemiesKilled>=8)
        {
            levelCompletedSuccessfully();
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            MapView.SetActive(true);
        }
        else MapView.SetActive(false);


        playerScore = healthData.RedHealth + healthData.BlueHealth * 2 - Mathf.FloorToInt(timeElapsed);

        timeElapsed += Time.deltaTime;
        BlueHealthText.text = "BLUE HEALTH : "  + healthData.BlueHealth.ToString();
        RedHealthText.text = "RED HEALTH : " +  healthData.RedHealth.ToString();
        EnemyKillCount.text = "ENEMIES KILLED : " +  enemiesKilled.ToString() + "/8";
        RedGemsCollected.text = "RED GEMS : " + healthData.RedCollection/2 + "/30";
        BlueGemsCollected.text = "BLUE GEMS : " + healthData.BlueCollection/2 + "/20";
        TimeElapsed.text = "TIME ELAPSED : " + timeElapsed.ToString("F0");
        CurrentScore.text = "Score : " + playerScore.ToString();
        

    }


    public void pauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResumeButton.SetActive(true);
        Time.timeScale = 0;
      
    }
    public void ResumeGame()
    {
  
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ResumeButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("level1");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Scene0");
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

    private void levelCompletedSuccessfully()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //playerScore = healthData.RedHealth + healthData.BlueHealth * 2 - Mathf.FloorToInt(timeElapsed);
        if (playerScore <= 10) playerScore = 10;
        score.text = "SCORE : " + playerScore.ToString();
        GameCompletedPrompt.SetActive(true);

    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        GameOverPrompt.SetActive(true);

    }
}
