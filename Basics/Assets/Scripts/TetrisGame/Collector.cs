using TMPro;
using UnityEngine;

public class TetrisCollector : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI counterText;

    [Header("Raccolta")]
    public int pezziRaccolti = 0;
    public int limitePezzi = 10;

    // Indica se puoi ancora raccogliere pezzi
    private bool canCollect = true;

    // Chiamare questo metodo per registrare la raccolta di un pezzo
    public void RaccogliPezzo()
    {
        if (!canCollect)
            return;

        pezziRaccolti++;
        AggiornaUI();

        if (pezziRaccolti >= limitePezzi)
        {
            canCollect = false;
            Debug.Log("Hai raggiunto il limite di pezzi raccolti!");
            // Qui puoi fare altre azioni come segnalare la fine livello, etc.
        }
    }

    private void AggiornaUI()
    {
        if (counterText != null)
        {
            counterText.text = $" {pezziRaccolti} / {limitePezzi}";
        }
    }

    private void Start()
    {
        AggiornaUI();
    }
}
