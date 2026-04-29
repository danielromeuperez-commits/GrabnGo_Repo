using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ammo : MonoBehaviour
{
    public TMP_Text ammo;
    public GunSystem GunSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        AmmoLeft();
    }

    // Update is called once per frame
    void AmmoLeft()
    {
        ammo.text = GunSystem.bulletsLeft + "/" + GunSystem.ammoSize;
    }
}
