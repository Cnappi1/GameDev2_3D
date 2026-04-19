using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessDialogFile
{
    private List<string> sceneText;
    private int currDialogIndex;
    private int endOfCurrOptionDialog;
    private string currOptionToken;
    private const int TEXT_CODE_LENGTH = 4;

    public AssessDialogFile() 
    {
        this.sceneText = new List<string>();
        this.currDialogIndex = 0;
        this.endOfCurrOptionDialog = 0;
        this.currOptionToken = "";
    }

    public string GetCurrLine() 
    {
        return this.sceneText[this.currDialogIndex];
    }

    public void ContinueToNextLine() 
    {
        if (this.isInOptionDialog())
        {
            this.currDialogIndex++;

            if (this.getCurrentToken() != this.getToken(this.currDialogIndex - 1))
            {
                while (this.getCurrentToken() != "[01]" && this.getCurrentToken() != "[02]" && this.getCurrentToken() != "[03]")
                {
                    this.currDialogIndex++;

                    if (this.currDialogIndex > this.sceneText.Count) 
                    {
                        return;
                    }
                }
            }
        }
        
        else
        {
            this.currDialogIndex++;
        }
        
    }

    private bool isInOptionDialog() 
    {
        return this.getCurrentToken() == "[04]" || this.getCurrentToken() == "[05]" || this.getCurrentToken() == "[06]";
    }

    public int GetCurrDialogIndex() 
    {
        return this.currDialogIndex;
    }

    public List<string> GetSceneText()
    {
        return this.sceneText;
    }

    public void SetSceneText(List<string> text) 
    {
        this.sceneText = text;
        this.currDialogIndex = 0;
    }

    public int TextCodeLength() 
    {
        return TEXT_CODE_LENGTH;
    }

    public void continueToOptionDialog(int chosenOption) 
    {
        int index = this.currDialogIndex;
        this.currOptionToken = "[" + 0 + "" + (3 + chosenOption) + "]";
        while (this.getToken(index) != this.currOptionToken)
        {
            index++;
        }

        if (index < this.sceneText.Count)
        {
            this.currDialogIndex = index;
        }

    }

    private string getToken(int index) 
    {
        if (index < this.sceneText.Count && this.sceneText[index].Length >= TEXT_CODE_LENGTH) 
        {
            return this.sceneText[index].Substring(0, TEXT_CODE_LENGTH);
        }

        return "";
    }

    private string getCurrentToken()
    {
        if (this.sceneText[this.currDialogIndex].Length >= TEXT_CODE_LENGTH)
        {
            return this.sceneText[this.currDialogIndex].Substring(0, TEXT_CODE_LENGTH);
        }

        return "";
    }
    
    public bool isActiveDialog()
    {
        if (currDialogIndex >= sceneText.Count - 1)
        {
            return false;
        }
        return this.getCurrentToken() == "[01]" || this.getCurrentToken() == "[03]";
    }

    public bool isChoiceDialog()
    {
        if (currDialogIndex >= sceneText.Count - 1)
        {
            return false;
        }
        return this.getCurrentToken() == "[03]";
    }
    
}
