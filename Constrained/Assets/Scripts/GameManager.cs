using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Disciple
    {
        public string[] dialouges;
        [HideInInspector]public int destiny;
        [HideInInspector]public int good;
        [HideInInspector]public int neutral;
        [HideInInspector]public int bad;
        public string[] goodChoices;
        public string[] neutralChoices;
        public string[] badChoices;
        public string[] verdicts;
    }

    [SerializeField] Disciple[] disciples;
    int discipleIndex = 0;
    Disciple currentDisciple;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public GameObject continueButton;
    string currentDialogue;
    int dialogueIndex = -1;
    public float typingSpeed;

    public GameObject choicePanel;
    public GameObject choiceButton;
    bool canChoose;

    public TMP_Text choice1;
    public TMP_Text choice2;
    public TMP_Text choice3;

    
    int choiceIndex = -1;

    public GameObject verdictPanel;
    public TMP_Text verdictText;

    public GameObject escapePanel;
    // Start is called before the first frame update
    void Start()
    {
        speakerText.text = "Master";
        dialogueText.text = "So, you wish to know your future?";
        currentDisciple = disciples[discipleIndex];
        currentDialogue = currentDisciple.dialouges[dialogueIndex+1];
        continueButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueText.text == currentDialogue)
        {
            continueButton.SetActive(true );
        }

        if(canChoose)
        {
            if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                currentDisciple.good++;
                currentDisciple.destiny++;
                canChoose = false;
                choicePanel.SetActive(false);
                choiceButton.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.Keypad2))
            {
                currentDisciple.neutral++;
                currentDisciple.destiny++;
                canChoose=false;
                choicePanel.SetActive(false);
                choiceButton.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.Keypad3))
            {
                currentDisciple.bad++;
                currentDisciple.destiny++;
                canChoose=false;
                choicePanel.SetActive(false);
                choiceButton.SetActive(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            escapePanel.SetActive(true);
        }
    }

    IEnumerator WriteSentence()
    {
        foreach(char c in currentDialogue.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextDialogue()
    {
        continueButton.SetActive(false);
        speakerText.text = "Disciple";
        if(dialogueIndex < currentDisciple.dialouges.Length-1)
        {
            dialogueIndex++;
            dialogueText.text = "";
            currentDialogue = currentDisciple.dialouges[dialogueIndex];
            StartCoroutine(WriteSentence());
        }
        else
        {
            speakerText.text = "Master";
            dialogueText.text = "What I say now will be your destiny...";
            choiceButton.SetActive(true);
        }
    }

    public void MakeChoices()
    {
        choiceButton.SetActive(false);
        if (choiceIndex < currentDisciple.goodChoices.Length - 1)
        {
            choiceIndex++;
            choicePanel.SetActive(true);
            choice1.text = currentDisciple.goodChoices[choiceIndex];
            choice2.text = currentDisciple.neutralChoices[choiceIndex];
            choice3.text = currentDisciple.badChoices[choiceIndex];
            canChoose = true;
        }
        else
        {
            if(currentDisciple.good > currentDisciple.neutral)
            {
                if(currentDisciple.good > currentDisciple.bad)
                {
                    verdictText.text = currentDisciple.verdicts[0];
                }
                else
                {
                    verdictText.text = currentDisciple.verdicts[1];
                }
            }
            else
            {
                if(currentDisciple.neutral > currentDisciple.bad)
                {
                    verdictText.text = currentDisciple.verdicts[2];
                }
                else
                {
                    verdictText.text = currentDisciple.verdicts[3];
                }
            }

            verdictPanel.SetActive(true);
        }
    }

    public void NextDisciple()
    {
        verdictPanel.SetActive(false);
        if(discipleIndex < disciples.Length - 1)
        {
            discipleIndex++;
            currentDisciple = disciples[discipleIndex];
            speakerText.text = "Master";
            dialogueText.text = "So, you wish to know your future?";
            dialogueIndex = -1;
            choiceIndex = -1;
            currentDialogue = currentDisciple.dialouges[dialogueIndex + 1];
            continueButton.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
