using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea] public string text;
    public float duration;
}

public class DialogManager : MonoBehaviour
{
    [Header("TMP per Personaggio 1")]
    public TMP_Text nameText1;       // Nome Todd o Cube (personaggio 1)
    public TMP_Text dialogueText1;   // Testo Todd o Cube

    [Header("TMP per Personaggio 2")]
    public TMP_Text nameText2;       // Nome Skelly
    public TMP_Text dialogueText2;   // Testo Skelly

    [Header("Prompt di fine dialogo")]
    public TMP_Text endPromptText;   // "Premi E per continuare"

    [Header("Dialoghi principali")]
    public DialogueLine[] lines;

    [Header("Dialogo cubo (Cube + Skelly)")]
    public GameObject todd;             // Todd da nascondere
    public GameObject skelly;           // Skelly (rimane sempre attiva)
    public GameObject cube;             // Cube da mostrare al posto di Todd

    public DialogueLine[] cubeLines;    // Dialoghi Cube+Skelly

    [Header("Scena da caricare alla fine del dialogo cubo")]
    public string nextSceneName;

    private int currentLine = 0;
    private bool isActive = false;
    private bool waitingAtEnd = false;
    private Coroutine autoNextCoroutine;

    private int cubeCurrentLine = 0;
    private bool cubeDialogActive = false;
    private Coroutine autoNextCoroutineCube;

    void Start()
    {
        ResetUI();
        isActive = true;
        currentLine = 0;
        ShowLine();

        if (cube != null)
            cube.SetActive(false);
    }

    void ResetUI()
    {
        endPromptText.gameObject.SetActive(false);
        ClearAllTexts();
    }

    void ClearAllTexts()
    {
        nameText1.text = "";
        dialogueText1.text = "";
        nameText2.text = "";
        dialogueText2.text = "";
    }

    void ShowLine()
    {
        ClearAllTexts();

        var line = lines[currentLine];

        if (line.characterName == "Todd")
        {
            nameText1.text = line.characterName;
            dialogueText1.text = line.text;
        }
        else if (line.characterName == "Skelly")
        {
            nameText2.text = line.characterName;
            dialogueText2.text = line.text;
        }
        else
        {
            Debug.LogWarning("Personaggio non riconosciuto: " + line.characterName);
        }

        if (autoNextCoroutine != null)
            StopCoroutine(autoNextCoroutine);

        autoNextCoroutine = StartCoroutine(AutoNextLine(line.duration));
    }

    IEnumerator AutoNextLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isActive && !waitingAtEnd && !cubeDialogActive)
            NextLine();
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine < lines.Length)
        {
            ShowLine();
        }
        else
        {
            WaitForEndConfirmation();
        }
    }

    void WaitForEndConfirmation()
    {
        waitingAtEnd = true;
        endPromptText.gameObject.SetActive(true);
        ClearAllTexts();
        endPromptText.text = "Press E to continue...";
    }

    void Update()
    {
        if (cubeDialogActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (autoNextCoroutineCube != null)
                    StopCoroutine(autoNextCoroutineCube);
                NextCubeLine();
            }
            return;
        }

        if (!isActive)
            return;

        if (waitingAtEnd)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCubeDialog();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (autoNextCoroutine != null)
                    StopCoroutine(autoNextCoroutine);
                NextLine();
            }
        }
    }

    void StartCubeDialog()
    {
        if (todd != null) todd.SetActive(false);
        if (cube != null) cube.SetActive(true);

        if (endPromptText != null)
            endPromptText.gameObject.SetActive(false);

        cubeDialogActive = true;
        cubeCurrentLine = 0;
        ShowCubeLine();
    }

    void ShowCubeLine()
    {
        if (cubeCurrentLine >= cubeLines.Length)
        {
            EndCubeDialog();
            return;
        }

        var line = cubeLines[cubeCurrentLine];

        if (line.characterName == "Cube Todd")
        {
            nameText1.text = line.characterName;
            dialogueText1.text = line.text;
            nameText2.text = "";
            dialogueText2.text = "";
        }
        else if (line.characterName == "Skelly")
        {
            nameText2.text = line.characterName;
            dialogueText2.text = line.text;
            nameText1.text = "";
            dialogueText1.text = "";
        }
        else
        {
            Debug.LogWarning("Personaggio non riconosciuto nel dialogo cubo: " + line.characterName);
            ClearAllTexts();
        }

        if (autoNextCoroutineCube != null)
            StopCoroutine(autoNextCoroutineCube);
        autoNextCoroutineCube = StartCoroutine(AutoNextCubeLine(line.duration));
    }

    IEnumerator AutoNextCubeLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (cubeDialogActive)
            NextCubeLine();
    }

    void NextCubeLine()
    {
        cubeCurrentLine++;
        ShowCubeLine();
    }

    void EndCubeDialog()
    {
        cubeDialogActive = false;
        ClearAllTexts();

        if (!string.IsNullOrEmpty(nextSceneName))
            SceneManager.LoadScene(nextSceneName);
    }
}
