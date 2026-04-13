using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Points Management")]
    [SerializeField] int actualPoints = 0;

    public int ActualPoints
    {
        get { return actualPoints; }
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}