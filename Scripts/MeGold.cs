using System;
using UnityEngine;

public class MeGold : MonoBehaviour
{
	private void Start()
	{
		for (int i = 0; i < 16; i++)
		{
			this.Num[i] = base.transform.FindChild("Num" + i).gameObject;
		}
	}

	private void Update()
	{
		this.FindNumGold();
		for (int i = 0; i < 16; i++)
		{
			if (FileManager.PlayerGold[i] > 9)
			{
				FileManager.PlayerGold[i] -= 10;
				FileManager.PlayerGold[i + 1]++;
			}
			if (FileManager.PlayerGold[i] < 0)
			{
				FileManager.PlayerGold[i + 1]--;
				FileManager.PlayerGold[i] += 10;
			}
		}
		for (int j = 0; j < 16; j++)
		{
			if (j > this.FirstNum)
			{
				this.Num[j].SetActive(false);
			}
			else
			{
				this.Num[j].SetActive(true);
			}
		}
		for (int k = 0; k < 16; k++)
		{
			if (FileManager.PlayerGold[k] % 10 >= 0 && FileManager.PlayerGold[k] % 10 < 10)
			{
				this.Num[k].GetComponent<SpriteRenderer>().sprite = FileManager.NumberImage[FileManager.PlayerGold[k] % 10];
			}
		}
	}

	private void FindNumGold()
	{
		int firstNum = 0;
		for (int i = 15; i >= 0; i--)
		{
			if (FileManager.PlayerGold[i] > 0)
			{
				firstNum = i;
				break;
			}
		}
		this.FirstNum = firstNum;
	}

	private GameObject[] Num = new GameObject[16];

	private int FirstNum;
}
