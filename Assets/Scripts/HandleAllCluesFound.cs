using UnityEngine;

public class HandleAllCluesFound : MonoBehaviour
{
    public TextAsset textFile;
    public PopulateDialogBox dialogBoxText;
    public GameObject clue1;
    public GameObject clue2;
    public GameObject clue3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var clue1Script = clue1.GetComponent<HandleTriggers>();
        var clue2Script = clue2.GetComponent<HandleTriggers>();
        var clue3Script = clue3.GetComponent<HandleTriggers>();

        if (this.AllCluesFound(clue1Script, clue2Script, clue3Script))
        {
            if (dialogBoxText.SetTextFile(textFile))
            {
                gameObject.SetActive(false);
            }
        }
    }

    private bool AllCluesFound(HandleTriggers script1, HandleTriggers script2, HandleTriggers script3)
    {
        return script1.HasBeenTriggered() && script2.HasBeenTriggered() && script3.HasBeenTriggered();
    }
}
