using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{

    private Queue<string> sentences;

    public TextMeshProUGUI dialogueText;

    [SerializeField]
    private float letterDisplayDelay = 0.1f;

    private bool startedTyping = false;

    private bool textComplete = false;

    private Dialogue currentDial;

    public float autoContinueTime;

    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dial)
    {
        dialogueText.text = "";
        sentences.Clear();

        Debug.Log("Started Dialogue");
        
        currentDial = dial;

        sentences.Clear();
        StartText();
    }

    public void StartText()
    {
        StopAllCoroutines();

        foreach (string sentence in currentDial.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {


        if (sentences.Count == 0)
        {
            EndText();
            return;
        }

        if (startedTyping)
        {
            return;
        }

        string newSentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(AutoScrollTimer());
        StartCoroutine(TypeSentence(newSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        startedTyping = true;

        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //audio text boops
            //AudioManager.instance.PlaySound()
            yield return new WaitForSeconds(letterDisplayDelay);
        }

        startedTyping = false;
        yield break;
    }

    IEnumerator AutoScrollTimer()
    {
        yield return new WaitForSeconds(autoContinueTime);

        DisplayNextSentence();
        yield break;
    }

    public void EndText()
    {
        //StopAllCoroutines();
        Debug.Log("EndText Called");
        dialogueText.text = "";
        sentences.Clear();
    }
}