using UnityEngine;

using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    public float velocidadRotacion = 200f;

    void Start()
    {
        // Rotación inicial de 90 grados
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    void OnMouseOver()
    {
        // Rotación continua al pasar el mouse
        transform.Rotate(velocidadRotacion * Time.deltaTime, 0, 0);
    }
}