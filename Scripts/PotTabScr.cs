using System;
using UnityEngine;

public class PotTabScr : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < 13; i++)
		{
			this.Level[i] = base.transform.FindChild("Level").transform.FindChild("Buy" + i).gameObject;
			this.Regen[i] = base.transform.FindChild("Regen").transform.FindChild("Buy" + i).gameObject;
			this.Limit[i] = base.transform.FindChild("Limit").transform.FindChild("Buy" + i).gameObject;
		}
		for (int j = 0; j < 14; j++)
		{
			this.LevelNumber[j] = base.transform.FindChild("Level").transform.FindChild("Cost").transform.FindChild("Num" + j).gameObject;
			this.RegenNumber[j] = base.transform.FindChild("Regen").transform.FindChild("Cost").transform.FindChild("Num" + j).gameObject;
			this.LimitNumber[j] = base.transform.FindChild("Limit").transform.FindChild("Cost").transform.FindChild("Num" + j).gameObject;
		}
	}

	private void Update()
	{
		this.BuyState();
		this.SettingCost();
		this.SpriteUpdate();
		PotTabScr.HideNumber(this.LevelCost, this.LevelNumber);
		PotTabScr.HideNumber(this.RegenCost, this.RegenNumber);
		PotTabScr.HideNumber(this.LimitCost, this.LimitNumber);
		if (Input.GetMouseButtonDown(0) && Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Buy")))
		{
			string name = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Buy")).collider.name;
			switch (name)
			{
			case "Level":
				if (PotTabScr.CanBuy(this.LevelCost) && FileManager.PotLevel < 14)
				{
					FileManager.PotLevel++;
					PotTabScr.Cost(this.LevelCost);
				}
				break;
			case "Regen":
				if (PotTabScr.CanBuy(this.RegenCost) && FileManager.PotRegen < 14)
				{
					FileManager.PotRegen++;
					PotTabScr.Cost(this.RegenCost);
				}
				break;
			case "Limit":
				if (PotTabScr.CanBuy(this.LimitCost) && FileManager.PotLimit < 14)
				{
					FileManager.PotLimit++;
					PotTabScr.Cost(this.LimitCost);
				}
				break;
			}
		}
	}

	public static void Cost(int[] Buy)
	{
		for (int i = 0; i < 14; i++)
		{
			FileManager.PlayerGold[i] -= Buy[i];
		}
		AudioSource.PlayClipAtPoint(FileManager.Buy, Vector3.zero, 0.7f);
		FileManager.DataSave();
	}

	public static bool CanBuy(int[] Buy)
	{
		for (int i = 15; i > 13; i--)
		{
			if (FileManager.PlayerGold[i] > 0)
			{
				return true;
			}
		}
		for (int j = 13; j >= 0; j--)
		{
			if (FileManager.PlayerGold[j] > Buy[j])
			{
				return true;
			}
			if (FileManager.PlayerGold[j] != Buy[j])
			{
				return false;
			}
		}
		return true;
	}

	private void BuyState()
	{
		for (int i = 0; i < 13; i++)
		{
			if (FileManager.PotLevel - 1 > i)
			{
				this.Level[i].gameObject.SetActive(true);
			}
			else
			{
				this.Level[i].gameObject.SetActive(false);
			}
			if (FileManager.PotRegen - 1 > i)
			{
				this.Regen[i].gameObject.SetActive(true);
			}
			else
			{
				this.Regen[i].gameObject.SetActive(false);
			}
			if (FileManager.PotLimit - 1 > i)
			{
				this.Limit[i].gameObject.SetActive(true);
			}
			else
			{
				this.Limit[i].gameObject.SetActive(false);
			}
		}
	}

	private void SpriteUpdate()
	{
		for (int i = 0; i < 14; i++)
		{
			this.LevelNumber[i].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[this.LevelCost[i] % 10];
			this.RegenNumber[i].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[this.RegenCost[i] % 10];
			this.LimitNumber[i].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[this.LimitCost[i] % 10];
		}
	}

	public static void HideNumber(int[] Temp, GameObject[] TempGo)
	{
		for (int i = 0; i < 14; i++)
		{
			if (i > PotTabScr.FindNumGold(Temp))
			{
				TempGo[i].SetActive(false);
			}
			else
			{
				TempGo[i].SetActive(true);
			}
		}
	}

	public static int FindNumGold(int[] First)
	{
		int result = 0;
		for (int i = 13; i >= 0; i--)
		{
			if (First[i] > 0)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private void SettingCost()
	{
		for (int i = 0; i < 14; i++)
		{
			this.LevelCost[i] = 0;
			this.RegenCost[i] = 0;
			this.LimitCost[i] = 0;
		}
		switch (FileManager.PotLevel)
		{
		case 1:
			this.LevelCost[1] = 2;
			this.LevelCost[0] = 5;
			break;
		case 2:
			this.LevelCost[2] = 2;
			this.LevelCost[1] = 1;
			this.LevelCost[0] = 5;
			break;
		case 3:
			this.LevelCost[3] = 1;
			this.LevelCost[2] = 6;
			this.LevelCost[1] = 5;
			break;
		case 4:
			this.LevelCost[4] = 1;
			this.LevelCost[3] = 3;
			this.LevelCost[2] = 5;
			break;
		case 5:
			this.LevelCost[5] = 1;
			this.LevelCost[4] = 1;
			this.LevelCost[3] = 5;
			break;
		case 6:
			this.LevelCost[5] = 9;
			this.LevelCost[4] = 7;
			this.LevelCost[3] = 5;
			break;
		case 7:
			this.LevelCost[6] = 8;
			this.LevelCost[5] = 8;
			break;
		case 8:
			this.LevelCost[7] = 8;
			this.LevelCost[6] = 2;
			break;
		case 9:
			this.LevelCost[8] = 7;
			this.LevelCost[7] = 9;
			break;
		case 10:
			this.LevelCost[9] = 7;
			this.LevelCost[8] = 9;
			break;
		case 11:
			this.LevelCost[10] = 8;
			this.LevelCost[9] = 1;
			this.LevelCost[8] = 5;
			break;
		case 12:
			this.LevelCost[11] = 8;
			this.LevelCost[10] = 8;
			break;
		case 13:
			this.LevelCost[12] = 9;
			this.LevelCost[11] = 7;
			this.LevelCost[10] = 5;
			break;
		}
		switch (FileManager.PotRegen)
		{
		case 1:
			this.RegenCost[1] = 1;
			this.RegenCost[0] = 5;
			break;
		case 2:
			this.RegenCost[2] = 1;
			this.RegenCost[1] = 3;
			this.RegenCost[0] = 5;
			break;
		case 3:
			this.RegenCost[3] = 1;
			this.RegenCost[1] = 5;
			break;
		case 4:
			this.RegenCost[3] = 9;
			this.RegenCost[2] = 3;
			this.RegenCost[1] = 5;
			break;
		case 5:
			this.RegenCost[4] = 8;
			this.RegenCost[3] = 5;
			break;
		case 6:
			this.RegenCost[5] = 7;
			this.RegenCost[4] = 7;
			this.RegenCost[3] = 5;
			break;
		case 7:
			this.RegenCost[6] = 7;
			this.RegenCost[5] = 5;
			break;
		case 8:
			this.RegenCost[7] = 7;
			this.RegenCost[6] = 6;
			break;
		case 9:
			this.RegenCost[8] = 8;
			break;
		case 10:
			this.RegenCost[9] = 8;
			this.RegenCost[8] = 6;
			break;
		case 11:
			this.RegenCost[11] = 1;
			break;
		case 12:
			this.RegenCost[12] = 1;
			this.RegenCost[11] = 2;
			break;
		case 13:
			this.RegenCost[13] = 1;
			this.RegenCost[12] = 5;
			this.RegenCost[11] = 5;
			break;
		}
		switch (FileManager.PotLimit)
		{
		case 1:
			this.LimitCost[1] = 1;
			this.LimitCost[0] = 0;
			break;
		case 2:
			this.LimitCost[1] = 8;
			this.LimitCost[0] = 5;
			break;
		case 3:
			this.LimitCost[2] = 7;
			break;
		case 4:
			this.LimitCost[3] = 5;
			this.LimitCost[2] = 8;
			break;
		case 5:
			this.LimitCost[4] = 5;
			this.LimitCost[3] = 1;
			break;
		case 6:
			this.LimitCost[5] = 4;
			this.LimitCost[4] = 4;
			this.LimitCost[3] = 5;
			break;
		case 7:
			this.LimitCost[6] = 4;
			this.LimitCost[5] = 2;
			break;
		case 8:
			this.LimitCost[7] = 4;
			this.LimitCost[6] = 1;
			break;
		case 9:
			this.LimitCost[8] = 4;
			this.LimitCost[7] = 1;
			break;
		case 10:
			this.LimitCost[9] = 4;
			this.LimitCost[8] = 1;
			this.LimitCost[7] = 5;
			break;
		case 11:
			this.LimitCost[10] = 4;
			this.LimitCost[9] = 5;
			this.LimitCost[8] = 5;
			break;
		case 12:
			this.LimitCost[11] = 4;
			this.LimitCost[10] = 8;
			this.LimitCost[9] = 5;
			break;
		case 13:
			this.LimitCost[12] = 5;
			this.LimitCost[11] = 4;
			this.LimitCost[10] = 5;
			break;
		}
	}

	private GameObject[] Level = new GameObject[13];

	private GameObject[] Regen = new GameObject[13];

	private GameObject[] Limit = new GameObject[13];

	private GameObject[] LevelNumber = new GameObject[14];

	private GameObject[] RegenNumber = new GameObject[14];

	private GameObject[] LimitNumber = new GameObject[14];

	private int[] LevelCost = new int[14];

	private int[] RegenCost = new int[14];

	private int[] LimitCost = new int[14];
}
