using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gravity behavior adapted from https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class CharacterGravity : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 characterVelocity = Vector3.zero;

    public float gravityValue = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && characterVelocity.y < 0)
        {
            characterVelocity.y = 0f;
        } 
        else
        {
            characterVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(characterVelocity * Time.deltaTime);
        }
    }
}
