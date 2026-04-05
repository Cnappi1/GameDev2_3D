using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class PopulateDialogBox : MonoBehaviour
{
    public TextAsset textFile;
    public GameObject player;
    public InputAction inputActions;
    public Button choiceButton1;
    public Button choiceButton2;
    [SerializeField] private TextMeshProUGUI output;

    private IList sceneText;
    private int dialogIndex = 0;
    private CharacterMovement playerMovement;
    private const int TEXT_CODE_LENGTH = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerMovement = player.gameObject.GetComponent<CharacterMovement>();
        this.choiceButton1.gameObject.SetActive(false);
        this.choiceButton2.gameObject.SetActive(false);
        this.sceneText = TextAssetToList(textFile);
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.enabled = !this.isActiveDialog();

        if (this.continueDialogKeyPressed() && !this.isChoiceDialog())
        {
            if (dialogIndex >= sceneText.Count - 1)
            {
                output.text = string.Empty;
                gameObject.SetActive(false);
                playerMovement.enabled = true;
            }
            else if (output.text == sceneText[dialogIndex].ToString().Substring(TEXT_CODE_LENGTH))
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                output.text = sceneText[dialogIndex].ToString().Substring(TEXT_CODE_LENGTH);
            }
        }

        if (isChoiceDialog())
        {
            this.setUpButtons();
        }
    }

    private bool continueDialogKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.KeypadEnter);
    }

    void StartDialogue()
    {
        output.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in sceneText[dialogIndex].ToString().Substring(TEXT_CODE_LENGTH))
        {
            output.text += c;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void NextLine()
    {
        if (dialogIndex < sceneText.Count - 1)
        {
            dialogIndex++;
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
        if (value <= 1)
        {
            NextLine();
            dialogIndex++;
            dialogIndex++;
        }
        else
        {
            dialogIndex++;
            NextLine();
        }

        this.choiceButton1.gameObject.SetActive(false);
        this.choiceButton2.gameObject.SetActive(false);
    }

    public bool SetTextFile(TextAsset textAsset)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
            this.textFile = textAsset;
            this.sceneText = TextAssetToList(textFile);
            this.dialogIndex = 0;
            return true;
        }
        return false;
    }

    private void displayDialog(string text)
    {
        this.output.text = text.Substring(4);
        this.dialogIndex++;
    }

    private bool isChoiceDialog()
    {
        if (dialogIndex >= sceneText.Count - 1)
        {
            return false;
        }
        string token = sceneText[dialogIndex].ToString().Substring(0, TEXT_CODE_LENGTH);
        return token == "[03]";
    }

    private bool isActiveDialog()
    {
        if (dialogIndex >= sceneText.Count - 1)
        {
            return false;
        }
        string token = sceneText[this.dialogIndex].ToString().Substring(0, TEXT_CODE_LENGTH);
        return token == "[01]" || token == "[03]";
    }

    private List<string> TextAssetToList(TextAsset ta)
    {
        return new List<string>(ta.text.Split('\n'));
    }

    private void setUpButtons()
    {
        this.choiceButton1.gameObject.SetActive(true);
        this.choiceButton2.gameObject.SetActive(true);
    }
}
