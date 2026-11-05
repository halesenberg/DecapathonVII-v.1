using UnityEngine;
using TMPro;

public class NPCDialogueAuto : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;  // Assegna il TextMeshProUGUI nel Inspector
    public string message = "";

    void Start()
    {
        dialogueText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueText.text = message;  // Mostra subito il messaggio appena entra
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueText.text = "";  // Pulisce il testo quando il player esce
        }
    }
}
