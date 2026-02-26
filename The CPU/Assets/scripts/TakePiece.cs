using System.Collections.Generic;
using UnityEngine;

public class TakePiece : MonoBehaviour
{
    [Header("Categorias na ordem correta")]
    // 0 = Case
    // 1 = Motherboard
    // 2+ = Demais peças
    [SerializeField] private List<Transform> categories;
    [SerializeField] private int progressIndex = 0;

    private void Start()
    {
        // Desativa todas as cópias no início
        foreach (Transform category in categories)
        {
            foreach (Transform piece in category)
            {
                piece.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("É o Player");
            if (other.GetComponent<PlayerHold>().heldObject != null)
            {
                ComputerPiece piece = other.GetComponent<PlayerHold>().heldObject.GetComponent<ComputerPiece>();
                TryPlacePiece(piece, other.gameObject);
            }
        }
    }

    private void TryPlacePiece(ComputerPiece piece, GameObject originalObject)
    {
        string categoryName = piece.category.ToString();

        // Verifica se a categoria atual é a correta
        if (progressIndex == 0 && categoryName != "Case")
        {
            Debug.Log("Você precisa colocar o Gabinete primeiro!");
            return;
        }

        if (progressIndex == 1 && categoryName != "Motherboard")
        {
            Debug.Log("Você precisa colocar a Motherboard agora!");
            return;
        }

        // Procura a categoria correspondente na lista
        foreach (Transform category in categories)
        {
            if (category.name == categoryName)
            {
                ActivateCorrectCopy(category, piece.name);

                progressIndex++;

                Destroy(originalObject);

                CheckCompletion();
                return;
            }
        }

        Debug.Log("Categoria não encontrada na mesa.");
    }

    private void ActivateCorrectCopy(Transform category, string pieceName)
    {
        foreach (Transform child in category)
        {
            if (child.name == pieceName)
                child.gameObject.SetActive(true);
            else
                child.gameObject.SetActive(false);
        }
    }

    private void CheckCompletion()
    {
        if (progressIndex >= categories.Count)
        {
            Debug.Log("PC MONTADO! 🎉");
        }
    }
}