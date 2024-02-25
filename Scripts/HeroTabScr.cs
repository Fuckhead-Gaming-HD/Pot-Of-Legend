using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroTabScr : MonoBehaviour
{
    static Dictionary<string, int> _003C_003Ef__switch_0024map0;
	public static void SetSelctIndex()
	{
		for (int i = 0; i < 10; i++)
		{
			GameObject.Find("HeroSelect" + i).GetComponent<SpriteRenderer>().enabled = false;
		}
		GameObject.Find("HeroSelect" + HeroTabScr.SelectIndex).GetComponent<SpriteRenderer>().enabled = true;
	}

	private void Start()
	{
		Shop.HeroTabStart = true;
		for (int i = 0; i < 10; i++)
		{
			this.Limit[i] = base.transform.FindChild("Limit").transform.FindChild("Buy" + i).gameObject;
		}
		for (int j = 0; j < 13; j++)
		{
			this.Weapon[j] = base.transform.FindChild("Weapon").transform.FindChild("Buy" + j).gameObject;
		}
		for (int k = 0; k < 14; k++)
		{
			this.WeaponNumber[k] = base.transform.FindChild("Weapon").transform.FindChild("Cost").transform.FindChild("Num" + k).gameObject;
			this.LimitNumber[k] = base.transform.FindChild("Limit").transform.FindChild("Cost").transform.FindChild("Num" + k).gameObject;
		}
		UnityEngine.Debug.Log("################################### init SelectIndex : " + HeroTabScr.SelectIndex + "################################### ");
		this.SettingCost();
		PotTabScr.HideNumber(this.WeaponCost, this.WeaponNumber);
		PotTabScr.HideNumber(this.MoveCost, this.LimitNumber);
		this.BuyState();
		this.SpriteUpdate();
	}

	private void Update()
	{
		this.BuyState();
		this.SettingCost();
		this.SpriteUpdate();
		PotTabScr.HideNumber(this.WeaponCost, this.WeaponNumber);
		PotTabScr.HideNumber(this.MoveCost, this.LimitNumber);
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Buy")))
			{
				string name = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Buy")).collider.name;
				if (name != null)
				{
					if (HeroTabScr._003C_003Ef__switch_0024map0 == null)
					{
						HeroTabScr._003C_003Ef__switch_0024map0 = new Dictionary<string, int>(2)
						{
							{
								"Weapon",
								0
							},
							{
								"Limit",
								1
							}
						};
					}
					int num;
					if (HeroTabScr._003C_003Ef__switch_0024map0.TryGetValue(name, out num))
					{
						if (num != 0)
						{
							if (num == 1)
							{
								if (PotTabScr.CanBuy(this.MoveCost) && FileManager.HeroLimit < 10)
								{
									FileManager.HeroLimit++;
									for (int i = 0; i < 10; i++)
									{
										base.transform.FindChild("Select").transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
									}
									GameObject.Find("HeroSelect" + (FileManager.HeroLimit - 1)).GetComponent<SpriteRenderer>().enabled = true;
									HeroTabScr.SelectIndex = FileManager.HeroLimit - 1;
									UnityEngine.Debug.Log("################################### 0 SelectIndex : " + HeroTabScr.SelectIndex + "################################### ");
									this.CharGen();
									PotTabScr.Cost(this.MoveCost);
								}
							}
						}
						else if (PotTabScr.CanBuy(this.WeaponCost) && FileManager.HeroWeapon[HeroTabScr.SelectIndex] < 13)
						{
							FileManager.HeroWeapon[HeroTabScr.SelectIndex]++;
							PotTabScr.Cost(this.WeaponCost);
						}
					}
				}
			}
			if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("HeroSelect")))
			{
				RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("HeroSelect"));
				if (int.Parse(raycastHit2D.collider.name.Replace("HeroSelect", string.Empty)) < FileManager.HeroLimit)
				{
					for (int j = 0; j < 10; j++)
					{
						base.transform.FindChild("Select").transform.GetChild(j).GetComponent<SpriteRenderer>().enabled = false;
					}
					raycastHit2D.collider.GetComponent<SpriteRenderer>().enabled = true;
					HeroTabScr.SelectIndex = int.Parse(raycastHit2D.collider.name.Replace("HeroSelect", string.Empty));
					UnityEngine.Debug.Log("################################### 1 SelectIndex : " + HeroTabScr.SelectIndex + "################################### ");
				}
			}
		}
	}

	private void BuyState()
	{
		for (int i = 0; i < 13; i++)
		{
			if (FileManager.HeroWeapon[HeroTabScr.SelectIndex] > i)
			{
				this.Weapon[i].gameObject.SetActive(true);
			}
			else
			{
				this.Weapon[i].gameObject.SetActive(false);
			}
		}
		for (int j = 0; j < 10; j++)
		{
			if (FileManager.HeroLimit > j)
			{
				this.Limit[j].gameObject.SetActive(true);
			}
			else
			{
				this.Limit[j].gameObject.SetActive(false);
			}
		}
	}

	private void SpriteUpdate()
	{
		for (int i = 0; i < 14; i++)
		{
			this.WeaponNumber[i].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[this.WeaponCost[i] % 10];
			this.LimitNumber[i].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[this.MoveCost[i] % 10];
		}
	}

	private void CharGen()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(ObjectManager.Char_Pab) as GameObject;
		gameObject.transform.parent = GameObject.Find("CharManager").transform;
		gameObject.transform.position = GameObject.Find("Door").transform.position;
		gameObject.GetComponent<Player>().Index = FileManager.HeroLimit - 1;
	}

	private void SettingCost()
	{
		for (int i = 0; i < 14; i++)
		{
			this.WeaponCost[i] = 0;
			this.MoveCost[i] = 0;
		}
		switch (FileManager.HeroLimit)
		{
		case 1:
			this.MoveCost[2] = 3;
			this.MoveCost[1] = 7;
			this.MoveCost[0] = 5;
			break;
		case 2:
			this.MoveCost[3] = 4;
			this.MoveCost[2] = 5;
			break;
		case 3:
			this.MoveCost[4] = 6;
			this.MoveCost[3] = 5;
			break;
		case 4:
			this.MoveCost[6] = 1;
			break;
		case 5:
			this.MoveCost[7] = 2;
			this.MoveCost[6] = 2;
			break;
		case 6:
			this.MoveCost[8] = 5;
			this.MoveCost[7] = 3;
			break;
		case 7:
			this.MoveCost[10] = 1;
			this.MoveCost[9] = 5;
			break;
		case 8:
			this.MoveCost[12] = 5;
			this.MoveCost[11] = 3;
			this.MoveCost[10] = 5;
			break;
		case 9:
			this.MoveCost[13] = 2;
			this.MoveCost[12] = 2;
			break;
		}
		switch (FileManager.HeroWeapon[HeroTabScr.SelectIndex])
		{
		case 0:
			this.WeaponCost[1] = 3;
			this.WeaponCost[0] = 5;
			break;
		case 1:
			this.WeaponCost[2] = 2;
			this.WeaponCost[1] = 7;
			this.WeaponCost[0] = 5;
			break;
		case 2:
			this.WeaponCost[3] = 2;
			this.WeaponCost[2] = 1;
			break;
		case 3:
			this.WeaponCost[4] = 1;
			this.WeaponCost[3] = 7;
			break;
		case 4:
			this.WeaponCost[5] = 1;
			this.WeaponCost[4] = 4;
			this.WeaponCost[3] = 5;
			break;
		case 5:
			this.WeaponCost[6] = 1;
			this.WeaponCost[5] = 2;
			this.WeaponCost[4] = 5;
			break;
		case 6:
			this.WeaponCost[7] = 1;
			this.WeaponCost[6] = 1;
			break;
		case 7:
			this.WeaponCost[8] = 1;
			this.WeaponCost[6] = 5;
			break;
		case 8:
			this.WeaponCost[9] = 1;
			break;
		case 9:
			this.WeaponCost[10] = 1;
			break;
		case 10:
			this.WeaponCost[11] = 1;
			break;
		case 11:
			this.WeaponCost[12] = 1;
			break;
		case 12:
			this.WeaponCost[13] = 1;
			this.WeaponCost[12] = 2;
			break;
		}
	}

	private GameObject[] Weapon = new GameObject[13];

	private GameObject[] Limit = new GameObject[10];

	private GameObject[] WeaponNumber = new GameObject[14];

	private GameObject[] LimitNumber = new GameObject[14];

	private int[] WeaponCost = new int[14];

	private int[] MoveCost = new int[14];

	public static int SelectIndex;
}
