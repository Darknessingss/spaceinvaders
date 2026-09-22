using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 6f;

    private void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        float input = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            input -= 1f;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            input += 1f;
        }

        if (input == 0f)
        {
            return;
        }

        transform.position += new Vector3(input * _moveSpeed * Time.deltaTime, 0f, 0f);
    }
}