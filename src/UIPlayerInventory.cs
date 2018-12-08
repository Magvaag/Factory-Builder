using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInventory : MonoBehaviour
{

	public Player Player;
	public RectTransform ObjectSelectedSlot;
	public List<ItemSlot> ItemSlots = new List<ItemSlot>();

	private bool _hasInit = false;
	
	// Use this for initialization
	void Init ()
	{
		if (Player.Inventory == null) return;
		_hasInit = true;
		for (var i = 0; i < ItemSlots.Count; i++)
		{
			ItemSlots[i].ItemStack = Player.Inventory.ItemStacks[i];
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!_hasInit) Init();
		ObjectSelectedSlot.anchoredPosition = new Vector3(60f * Player.GetSelectedSlot(), 0);
	}
}
