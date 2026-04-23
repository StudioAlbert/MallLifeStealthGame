using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealable : MonoBehaviour
{
    [SerializeField] private StealableItemSO _item;
    [SerializeField] private InventorySO _inventory;
    [SerializeField] private ActionDescriptor _descriptor;

    [SerializeField] private List<Component> _lockableComponents;

    public StealableItemSO Item => _item;
    public ActionDescriptor Descriptor => _descriptor;

    public void Steal()
    {
        _inventory.AddItem(_item);

        SetLockables(false);
        StartCoroutine(Unlock());

    }

    private IEnumerator Unlock()
    {
        yield return new WaitForSeconds(_descriptor.ReliveTime);
        SetLockables(true);
    }

    private void SetLockables(bool unlockState)
    {
        foreach (var component in _lockableComponents)
        {
            switch (component)
            {
                case Renderer r: r.enabled = unlockState; break;
                case Collider c: c.enabled = unlockState; break;
                case Behaviour b: b.enabled = unlockState; break;
            }
        }
    }
}
