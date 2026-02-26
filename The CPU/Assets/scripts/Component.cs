using UnityEngine;

public enum PieceCategory
{
    Case,
    Motherboard,
    CPU,
    RAM,
    GPU,
}

public class ComputerPiece : MonoBehaviour
{
    public PieceCategory category;
}