using UnityEngine;
using TMPro;

public class Dialog_AnneRoom : MonoBehaviour
{
    public TextAsset textFile;
    public TextMeshProUGUI output;
    public GameObject dialogBox;

    private string[] lines;
    private int index = 0;
    private bool isActive = false;

    void Start()
    {
        if (textFile != null)
        {
            lines = textFile.text.Split('\n');
        }

        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive && lines != null && lines.Length > 0)
        {
            StartDialog();
        }
    }

    void StartDialog()
    {
        isActive = true;
        dialogBox.SetActive(true);
        index = 0;
        output.text = CleanLine(lines[index]);
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            output.text = CleanLine(lines[index]);
        }
        else
        {
            dialogBox.SetActive(false);
            isActive = false;
        }
    }

    string CleanLine(string line)
    {
        line = line.Trim();

        if (line.Length >= 4 && line[0] == '[' && line[3] == ']')
        {
            return line.Substring(4).Trim();
        }

        return line;
    }
}