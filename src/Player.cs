using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{

	public Camera Camera;
	public GameObject HoverSelect;
	[HideInInspector]
	public Inventory Inventory;

	public FirstPersonController PersonController;
	
	private float highlightDistance = 6;
	private int selectedSlot;
	
	// Use this for initialization
	void Start ()
	{
		PersonController = GetComponent<FirstPersonController>();
		Inventory = new Inventory(9);
		Inventory.AddItemStack(new ItemStack(Item.CONVEYOR_BELT, 100));
		Inventory.AddItemStack(new ItemStack(Item.CHEST, 20));
	}
	
	// Update is called once per frame
	void Update ()
	{
		var constructHover = GetConstructHover();
		if (constructHover != null)
		{
			HoverSelect.transform.position = constructHover.transform.position;
			HoverSelect.SetActive(true);
		}
		else
		{
			HoverSelect.SetActive(false);
		}

		if (Input.GetMouseButtonDown(1) && constructHover != null)
		{
			constructHover.OnConstructRightClick(this);
		}
		
		if (Input.GetMouseButtonDown(0) && constructHover != null)
		{
			constructHover.OnConstructLeftClick(this);
		}

		CheckScrollWheelInventorySlot();
	}

	private void CheckScrollWheelInventorySlot()
	{
		var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheel > 0) selectedSlot--;
		else if (scrollWheel < 0) selectedSlot++;
		while (selectedSlot < 0) selectedSlot += 9;
		while (selectedSlot >= 9) selectedSlot -= 9;
	}

	private Construct GetConstructHover()
	{
		var layerMask = ~(1 << 9); // We don't want to hit ourselves. Bit-mask invert the player mask
		RaycastHit hit;
		if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, highlightDistance, layerMask))
		{
			var construct = hit.collider.GetComponent<Construct>();
			if (construct == null)
			{
				Vector3 flatPos =  hit.point;
				flatPos.x = (float) Math.Round(flatPos.x);
				flatPos.y = (float) Math.Round(flatPos.y);
				flatPos.z = (float) Math.Round(flatPos.z);
				HoverSelect.transform.position = flatPos;
				return HoverSelect.GetComponent<Construct>();
			}
			return construct;
		}

		return null;
	}

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.None; //unlock cursor
		Cursor.visible = true; //make mouse visible
		PersonController.LockView = true;
		PersonController.m_MouseLook.m_cursorIsLocked = false;
		//ExecuteEvents.Execute(gameObject, "OnAllowGameplayInput", false); //disable TPC input
	}

	public void UnlockCursor()
	{
		//PersonController.LockView = false;
		Cursor.lockState = CursorLockMode.Locked; //unlock cursor
		Cursor.visible = false; //make mouse visible
		PersonController.LockView = false;
		PersonController.m_MouseLook.m_cursorIsLocked = true;
		//Cursor.visible = false;
	}

	public int GetSelectedSlot()
	{
		return selectedSlot;
	}
}
