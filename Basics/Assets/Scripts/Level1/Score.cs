using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;
    public int scoreTarget = 500;
    public GameObject playerObject;

    private bool levelEnded = false;

    void Update()
    {
        if (player != null && scoreText != null && !levelEnded)
        {
            int score = Mathf.FloorToInt(player.position.z);
            scoreText.text = score.ToString();

            if (score >= scoreTarget)
            {
                EndLevel();
            }
        }
    }

    void EndLevel()
    {
        levelEnded = true;

        if (playerObject != null)
        {
            playerObject.SetActive(false);
        }

        scoreText.text = "You Won";

        // Fai partire il suono di vittoria
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayVictorySound();
        }

        Invoke("LoadNextLevel", 2f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
