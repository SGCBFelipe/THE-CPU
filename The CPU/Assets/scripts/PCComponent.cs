using UnityEngine;

[CreateAssetMenu(menuName = "PC Component")]
public class PCComponent : ScriptableObject
{
    public string nome;

    public enum Tipo
    {
        Motherboard,
        RAM,
        Fonte,
        Cooler,
        CPU,
        GPU,
        NVME
    }

    public Tipo PCType = Tipo.Motherboard;  


    public GameObject modelo;


}
