using System;
using UnityEngine;

public class Pot : MonoBehaviour
{
	private void Start()
	{
		this.PotLevel();
		base.GetComponent<SpriteRenderer>().sprite = FileManager.PotImage[this.ValueLevel];
	}

	private void Update()
	{
		if (this.HP <= 0)
		{
			this.PotDestroy();
		}
	}

	private void PotDestroy()
	{
		for (int i = 0; i < 14; i++)
		{
			GameObject[] array = new GameObject[this.GoldLevel[i]];
			for (int j = 0; j < this.GoldLevel[i]; j++)
			{
				array[j] = (UnityEngine.Object.Instantiate(ObjectManager.Gold_Pab) as GameObject);
				array[j].GetComponent<SpriteRenderer>().sprite = FileManager.GoldImage[i];
				array[j].GetComponent<Gold>().ValueLevel = i;
				array[j].transform.position = base.transform.position;
				array[j].GetComponent<Rigidbody2D>().AddForce(new Vector2((float)(UnityEngine.Random.Range(0, 600) - 300), (float)UnityEngine.Random.Range(200, 700)));
				array[j].transform.parent = GameObject.Find("GoldManager").transform;
			}
		}
		Ranking.Rank_DestroyPot++;
		AudioSource.PlayClipAtPoint(FileManager.Destroy, Vector3.zero, 0.4f);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void PotLevel()
	{
		switch (this.ValueLevel)
		{
		case 0:
			this.GoldLevel[0] = 5;
			this.HP = 3;
			break;
		case 1:
			this.GoldLevel[1] = 3;
			this.GoldLevel[0] = 6;
			this.HP = 8;
			break;
		case 2:
			this.GoldLevel[2] = 2;
			this.GoldLevel[1] = 7;
			this.GoldLevel[0] = 3;
			this.HP = 21;
			break;
		case 3:
			this.GoldLevel[3] = 2;
			this.GoldLevel[2] = 1;
			this.GoldLevel[1] = 2;
			this.GoldLevel[0] = 4;
			this.HP = 56;
			break;
		case 4:
			this.GoldLevel[4] = 1;
			this.GoldLevel[3] = 7;
			this.GoldLevel[2] = 1;
			this.GoldLevel[1] = 1;
			this.HP = 151;
			break;
		case 5:
			this.GoldLevel[5] = 1;
			this.GoldLevel[4] = 4;
			this.GoldLevel[3] = 3;
			this.GoldLevel[2] = 2;
			this.HP = 407;
			break;
		case 6:
			this.GoldLevel[6] = 1;
			this.GoldLevel[5] = 2;
			this.GoldLevel[4] = 4;
			this.GoldLevel[3] = 3;
			this.HP = 1098;
			break;
		case 7:
			this.GoldLevel[7] = 1;
			this.GoldLevel[6] = 1;
			this.GoldLevel[5] = 1;
			this.GoldLevel[4] = 7;
			this.HP = 2964;
			break;
		case 8:
			this.GoldLevel[8] = 1;
			this.GoldLevel[6] = 4;
			this.GoldLevel[4] = 4;
			this.GoldLevel[3] = 1;
			this.HP = 8002;
			break;
		case 9:
			this.GoldLevel[9] = 1;
			this.GoldLevel[6] = 3;
			this.GoldLevel[5] = 6;
			this.HP = 21605;
			break;
		case 10:
			this.GoldLevel[10] = 1;
			this.GoldLevel[8] = 3;
			this.GoldLevel[7] = 6;
			this.HP = 58333;
			break;
		case 11:
			this.GoldLevel[11] = 1;
			this.GoldLevel[9] = 4;
			this.GoldLevel[7] = 1;
			this.GoldLevel[6] = 3;
			this.GoldLevel[5] = 1;
			this.HP = 157499;
			break;
		case 12:
			this.GoldLevel[12] = 1;
			this.GoldLevel[11] = 1;
			this.GoldLevel[10] = 1;
			this.GoldLevel[9] = 6;
			this.GoldLevel[8] = 1;
			this.HP = 425247;
			break;
		case 13:
			this.GoldLevel[13] = 1;
			this.GoldLevel[12] = 2;
			this.GoldLevel[11] = 4;
			this.GoldLevel[10] = 2;
			this.GoldLevel[9] = 3;
			this.HP = 1148166;
			break;
		}
	}

	public int HP;

	public int ValueLevel;

	public int Flour;

	public bool Pick;

	private int[] GoldLevel = new int[14];
}
