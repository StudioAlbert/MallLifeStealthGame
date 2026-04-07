using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MenusManager : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private InputMenus _inputMenus;
    [SerializeField] private GameObject _inventoryView;

    private void OnEnable()
    {
        _inventoryView.SetActive(false);
    }
    private void OnDisable()
    {
        _inventoryView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputMenus.InventoryUp) SwitchView(_inventoryView);
    }

    private void SwitchView(GameObject view)
    {
        Debug.Log($"Am i switching view ? {view.name}, current state {view.activeSelf}");
        view.SetActive(!view.activeSelf);
    }
}
