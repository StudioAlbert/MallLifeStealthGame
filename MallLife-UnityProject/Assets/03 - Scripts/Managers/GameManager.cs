using UnityEngine;


public class GameManager : MonoBehaviour
{

	[SerializeField] private InventorySO _playerInventory;

	void OnEnable() => _playerInventory.Empty();
}
