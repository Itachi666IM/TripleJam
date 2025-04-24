using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Story : MonoBehaviour
{
    public TMP_Text storyText;
    public string[] storyTextsToWrite;
    int index = 0;
    string currentStoryText;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject startGameButton;

    public Animator anim;

    private void Start()
    {
        currentStoryText = storyTextsToWrite[index];
        StartCoroutine(WriteSentence());
    }
    private void Update()
    {
        if (storyText.text == currentStoryText)
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator WriteSentence()
    {
        foreach (char c in currentStoryText.ToCharArray())
        {
            storyText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        anim.SetTrigger("change");
        if (index < storyTextsToWrite.Length - 1)
        {
            index++;
            storyText.text = "";
            currentStoryText = storyTextsToWrite[index];
            StartCoroutine(WriteSentence());
        }
        else
        {
            storyText.text = "Whether you be good or evil, the choice is yours...";
            continueButton.SetActive(false);
            startGameButton.SetActive(true);
        }
    }
}
