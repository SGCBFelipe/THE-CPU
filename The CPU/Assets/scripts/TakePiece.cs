using System.Collections.Generic;
using UnityEngine;

public class TakePiece : MonoBehaviour
{
    [Header("Categorias na ordem correta")]
    // 0 = Case
    // 1 = Motherboard
    // 2+ = Demais peças
    [SerializeField] private List<Transform> categories;

    private List<bool> categoryPlaced = new List<bool>();
    private int progressIndex = 0;

    private void Start()
    {
        categoryPlaced.Clear();

        for (int i = 0; i < categories.Count; i++)
        {
            categoryPlaced.Add(false);

            // Desativa todas as cópias no início
            foreach (Transform piece in categories[i])
            {
                piece.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerHold playerHold = other.GetComponent<PlayerHold>();

        if (playerHold == null || playerHold.heldObject == null)
            return;

        GameObject heldItem = playerHold.heldObject;
        ComputerPiece piece = heldItem.GetComponent<ComputerPiece>();

        if (piece == null)
            return;

        bool placedSuccessfully = TryPlacePiece(piece, heldItem);

        if (placedSuccessfully)
        {
            playerHold.heldObject = null;
        }
    }

    private bool TryPlacePiece(ComputerPiece piece, GameObject originalObject)
    {
        string categoryName = piece.category.ToString();

        for (int i = 0; i < categories.Count; i++)
        {
            if (categories[i].name == categoryName)
            {
                // 🚫 Já foi colocada?
                if (categoryPlaced[i])
                {
                    Debug.Log("Essa peça já foi colocada!");
                    return false;
                }

                // 🚫 Está fora da ordem?
                if (i != progressIndex)
                {
                    if (progressIndex < categories.Count)
                        Debug.Log("Você precisa colocar: " + categories[progressIndex].name + " primeiro!");
                    return false;
                }

                // ✅ Ativa cópia correta
                ActivateCorrectCopy(categories[i], piece.name);

                categoryPlaced[i] = true;
                progressIndex++;

                Destroy(originalObject);

                CheckCompletion();
                return true;
            }
        }

        Debug.Log("Categoria não encontrada na mesa.");
        return false;
    }

    private void ActivateCorrectCopy(Transform category, string pieceName)
    {
        foreach (Transform child in category)
        {
            child.gameObject.SetActive(child.name == pieceName);
        }
    }

    private void CheckCompletion()
    {
        foreach (bool placed in categoryPlaced)
        {
            if (!placed)
                return;
        }

        Debug.Log("PC MONTADO! 🎉");
    }
}