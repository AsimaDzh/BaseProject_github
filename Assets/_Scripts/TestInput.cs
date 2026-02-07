using UnityEngine;

public class TestInput : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("TestInput: No InputManager instance found!");
            return;
        }

        Vector2 move = InputManager.Instance.MoveInput;
        Vector2 look = InputManager.Instance.LookInput;

        if (move != Vector2.zero) Debug.Log($"Move Input: {move}");
        if (look != Vector2.zero) Debug.Log($"Look Input: {look}");

        if (InputManager.Instance.JumpPressed) Debug.Log("Jump Pressed");
        if (InputManager.Instance.AttackPressed) Debug.Log("Attack Pressed");
        if (InputManager.Instance.InteractPressed) Debug.Log("Interact Pressed");

        InputManager.Instance.ResetButtonFlags();
    }
}
