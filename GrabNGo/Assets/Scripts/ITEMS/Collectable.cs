using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Points Management")]
    [SerializeField] public int actualPoints;

    // Propiedad pública de solo lectura para otros scripts
    public int ActualPoints
    {
        get { return actualPoints; }
    }

    // Start is called before the first frame update
    void Start()
    {
        actualPoints = 0;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            actualPoints += 1;
            collision.gameObject.SetActive(false);
        }
    }
}
