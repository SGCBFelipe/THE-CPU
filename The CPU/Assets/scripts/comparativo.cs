using UnityEngine;

public class comparativo : MonoBehaviour
{
    public GameObject[] computerPieces;

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject CP in computerPieces)
        {
            if (other == CP)
            {
                Debug.Log("Verdadeiro");
            }
            else
            {
                Debug.Log("Falso");
            }
        }
    }
}
