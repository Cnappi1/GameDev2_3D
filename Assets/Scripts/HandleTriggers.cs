using UnityEngine;

public class HandleTriggers : MonoBehaviour
{
    public TextAsset textFile;
    public PopulateDialogBox dialogBoxText;

    private bool hasBeenTriggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasBeenTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !hasBeenTriggered) 
        {
            dialogBoxText.SetTextFile(textFile);
            hasBeenTriggered = true;
        }
    }

    public bool HasBeenTriggered()
    {
        return hasBeenTriggered;
    }
}
