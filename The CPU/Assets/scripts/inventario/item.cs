using UnityEngine;

public class item : MonoBehaviour
{
    public string idItem;

    public void OnCollisionEnter(Collision collision)
    {
            /*if (collision.gameObject.tag == "Player")
            inventario Inventario = collision.gameObject.GetComponent<inventario>();
            bool adicinou = Inventario.addItem(idItem);
            if (adicinou )
                Destroy(gameObject);*/
        
    }


}
