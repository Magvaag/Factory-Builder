using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class UIInventoryChest : MonoBehaviour
{

	public Game Game;
	public GameObject PrefabItemSlot;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}

	public void Show(Inventory inventory)
	{
		gameObject.SetActive(true);
		foreach (var itemStack in inventory.ItemStacks)
		{
			var go = Instantiate(PrefabItemSlot, transform);
			var itemSlot = go.GetComponent<ItemSlot>();
			itemSlot.ItemStack = itemStack;
		}

		Game.Player.LockCursor();
	}

	public void Close()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		
		gameObject.SetActive(false);
		Game.Player.UnlockCursor();
	}
}
