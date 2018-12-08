using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{

	public TextMeshProUGUI TextAmount;
	public ItemStack ItemStack;
	public GameObject RenderObjectParent;
	private Item _lastItem;
	
	// Use this for initialization
	void Start ()
	{
		UpdateRenderObject();
	}
	
	// Update is called once per frame
	void Update ()
	{
		var amount = 0;
		if (ItemStack != null)
		{
			amount = ItemStack.amount;
			if (_lastItem != ItemStack.item) UpdateRenderObject();
		}
		TextAmount.text = amount + "";

		
	}

	void UpdateRenderObject()
	{
		_lastItem = ItemStack != null ? ItemStack.item : null;
		
		foreach (Transform child in RenderObjectParent.transform)
		{
			Destroy(child.gameObject);
		}

		if (ItemStack != null && ItemStack.item.RenderObject != null)
		{
			var go = Instantiate(ItemStack.item.RenderObject, RenderObjectParent.transform);
		}
	}
}
