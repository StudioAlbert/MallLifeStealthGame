using System.Collections;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    [SerializeField] private StealableItem _item;
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private ActionDescriptor _descriptor;

    public StealableItem Item => _item;
    public ActionDescriptor Descriptor => _descriptor;
    
    public void Steal()
    {
        _inventory.AddItem(_item);
        //Destroy(gameObject);
        gameObject.SetActive(false);
        //StartCoroutine(Relive());
    }
    
    private IEnumerator Relive()
    {
        if(gameObject.activeSelf) yield break;
        
        yield return new WaitForSeconds(_descriptor.ReliveTime);
        gameObject.SetActive(true);
    }
}
