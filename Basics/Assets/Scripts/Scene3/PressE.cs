using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PressE : MonoBehaviour
{
    public TextMeshProUGUI dialogoText;   // Il testo del player (TMP)
    public TextMeshProUGUI promptText;    // Il messaggio "Premi E" (TMP)

    private bool puoiPremereE = false;

    void Start()
    {
        if (dialogoText != null)
        {
            dialogoText.text = "It's not funny anymore. Let me out of this game!!";
        }

        if (promptText != null)
        {
            promptText.text = "";  // All'inizio il messaggio è vuoto
        }

        Invoke("MostraPremiE", 2f);  // Dopo 2 secondi compare il prompt
    }

    void MostraPremiE()
    {
        if (promptText != null)
        {
            promptText.text = "Press E to go back to the real world";
        }
        puoiPremereE = true;
    }

    void Update()
    {
        if (puoiPremereE && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
