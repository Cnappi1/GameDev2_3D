using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using static AssessDialogFile;

public class PopulateDialogBox : MonoBehaviour
{
    public TextAsset textFile;
    public GameObject player;
    public InputActionReference continueDialog;
    public Button choiceButton1;
    public Button choiceButton2;
    public Button choiceButton3;
    [SerializeField] private TextMeshProUGUI output;
    private CharacterMovement playerMovement;
    private AssessDialogFile textAssessor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.textAssessor = new AssessDialogFile();
        this.playerMovement = player.gameObject.GetComponent<CharacterMovement>();
        this.choiceButton1.gameObject.SetActive(false);
        this.choiceButton2.gameObject.SetActive(false);
        this.choiceButton3.gameObject.SetActive(false);
        this.textAssessor.SetSceneText(this.TextAssetToList(this.textFile));
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.enabled = !this.textAssessor.isActiveDialog();
    }

    void StartDialogue()
    {
        output.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in this.textAssessor.GetCurrLine().Substring(this.textAssessor.TextCodeLength()))
        {
            output.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnEnable()
    {
        continueDialog.action.performed += OnContinueDialog;
        continueDialog.action.Enable();
    }

    private void OnDisable()
    {
        continueDialog.action.performed -= OnContinueDialog;
        continueDialog.action.Disable();
    }

    void OnContinueDialog(InputAction.CallbackContext context)
    {
        if (!this.textAssessor.isChoiceDialog())
        {
            if (this.textAssessor.GetCurrDialogIndex() > this.textAssessor.GetSceneText().Count)
            {
                output.text = string.Empty;
                gameObject.SetActive(false);
                playerMovement.enabled = true;
            }
            else if (this.textAssessor.GetCurrDialogIndex() < this.textAssessor.GetSceneText().Count && output.text == this.textAssessor.GetCurrLine().ToString().Substring(this.textAssessor.TextCodeLength()))
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                output.text = this.textAssessor.GetCurrLine().ToString().Substring(this.textAssessor.TextCodeLength());
            }
        }

        if (this.textAssessor.isChoiceDialog())
        {
            this.activateButtons(true);
        }
    }

    private bool continueDialogKeyPressed()
    {
        return false;
    }

    void NextLine()
    {
        this.textAssessor.ContinueToNextLine();

        if (this.textAssessor.GetCurrDialogIndex() < this.textAssessor.GetSceneText().Count)
        {
            output.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            output.text = string.Empty;
            gameObject.SetActive(false);
            playerMovement.enabled = true;
        }
    }

    public void ChoseOption(int value)
    {
        this.textAssessor.continueToOptionDialog(value);
        this.activateButtons(false);
        StopAllCoroutines();   
        output.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public bool SetTextFile(TextAsset textAsset)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            this.textFile = textAsset;
            this.textAssessor.SetSceneText(this.TextAssetToList(textAsset));
            return true;
        }
        return false;
    }

    private List<string> TextAssetToList(TextAsset ta)
    {
        return new List<string>(ta.text.Split('\n'));
    }

    private void activateButtons(bool value) 
    {
        this.choiceButton1.gameObject.SetActive(value);
        this.choiceButton2.gameObject.SetActive(value);
        this.choiceButton3.gameObject.SetActive(value);
    }
}
