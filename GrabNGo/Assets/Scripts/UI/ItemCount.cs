using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCount : MonoBehaviour
{
    public TMP_Text contadorText;
    public Collectable playerCollectable;
    public int totalItems;

    void Update()
    {
        ActualizarUI();
    }

    void ActualizarUI()
    {
        contadorText.text = playerCollectable.ActualPoints + "/" + totalItems;
    }
}