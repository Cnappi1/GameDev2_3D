using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Rotates the player in the horizontal plane, e.g.,
/// "look side to side", using mouse look, etc.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerTurnLeftRight : MonoBehaviour
{
    public float sensitivityX = 75f;

    float mouseX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        Vector2 delta = value.Get<Vector2>();
        mouseX = delta.x * sensitivityX;
    }
}
