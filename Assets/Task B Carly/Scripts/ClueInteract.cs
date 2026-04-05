using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ClueInteract : MonoBehaviour
{
    public string clueText = "Clue Found!";
    public TextMeshProUGUI clueMessage;

    private GameObject player;
    private bool playerInRange = false;

    private static ClueInteract activeClue;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (clueMessage != null)
        {
            clueMessage.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        playerInRange = distance < 5f;

        if (!playerInRange)
        {
            if (activeClue == this && clueMessage != null)
            {
                clueMessage.gameObject.SetActive(false);
                activeClue = null;
            }

            return;
        }

        if ((Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) ||
            (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame))

        {
            Debug.Log(clueText);

            if (clueMessage != null)
            {
                clueMessage.text = clueText;
                clueMessage.gameObject.SetActive(true);
                activeClue = this;
            }
        }
    }
}



