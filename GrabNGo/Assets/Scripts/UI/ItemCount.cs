using UnityEngine;
using TMPro;

public class ItemCount : MonoBehaviour
{
    public TMP_Text contadorText;
    public PlayerController player;
    public int totalItems;

    void Update()
    {
        ActualizarUI();
    }

    void ActualizarUI()
    {
        contadorText.text = player.TotalPoints + "/" + totalItems;
    }
}