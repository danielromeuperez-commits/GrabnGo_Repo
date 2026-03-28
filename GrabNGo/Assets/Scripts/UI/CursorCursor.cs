using UnityEngine;

public class Cursorunlock : MonoBehaviour
{
    void Start()
    {
        // Mostrar el cursor
        Cursor.visible = true;

        // Desbloquear el cursor
        Cursor.lockState = CursorLockMode.None;
    }
}