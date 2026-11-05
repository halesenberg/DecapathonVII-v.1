using UnityEngine;
using TMPro;

public class NPCDialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Il testo principale dei dialoghi
    public string message = "Ciao, benvenuto nel mio mondo!";

    public TextMeshProUGUI interactionText;  // "Premi E per parlare..."

    private bool playerInRange = false;

    void Start()
    {
        if (dialogueText != null)
            dialogueText.text = "";

        if (interactionText != null)
            interactionText.text = "";
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueText.text = message;
            Invoke("NascondiMessaggio", 3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactionText != null)
                interactionText.text = "Press E to talk...";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactionText != null)
                interactionText.text = "";
            if (dialogueText != null)
                dialogueText.text = "";
        }
    }

    void NascondiMessaggio()
    {
        if (dialogueText != null)
            dialogueText.text = "";
    }
}
