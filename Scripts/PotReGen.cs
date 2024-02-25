using System;
using UnityEngine;

public class PotReGen : MonoBehaviour
{
	private void Start()
	{
		this.Manager = GameObject.Find("PotManager");
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (!Shop.OnShop)
			{
				if (Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Shop")))
				{
					AudioSource.PlayClipAtPoint(FileManager.Shop, Vector3.zero);
					Shop.OnShop = true;
					if (Shop.HeroTabStart)
					{
						HeroTabScr.SetSelctIndex();
					}
					if (Application.platform == RuntimePlatform.Android)
					{
						UnityEngine.Debug.Log("---------------------ShowAds");
						AdvertisementHandler.ShowAds();
					}
					else
					{
						UnityEngine.Debug.Log("---------------------Application.platform != RuntimePlatform.Android");
					}
				}
				else
				{
					if (PotReGen.Regen < 10f)
					{
						Ranking.Rank_Touch++;
						AudioSource.PlayClipAtPoint(FileManager.Cm, Vector3.zero, 0.2f);
					}
					PotReGen.Regen += 0.5f + (float)FileManager.PotRegen * 0.32f;
					PotReGen.GageAni += 0.5f + (float)FileManager.PotRegen * 0.32f;
				}
			}
			else if (Shop.FullShop && Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("ShopExit")))
			{
				AudioSource.PlayClipAtPoint(FileManager.Shop, Vector3.zero, 0.7f);
				Shop.OnShop = false;
				if (Application.platform == RuntimePlatform.Android)
				{
					AdvertisementHandler.HideAds();
				}
			}
		}
		if (!Shop.OnShop)
		{
			if (PotReGen.Regen > 10f)
			{
				if (FileManager.PotLimit * 2 > this.Manager.transform.childCount)
				{
					PotReGen.Regen -= 10f;
					this.PotGen();
				}
				else
				{
					PotReGen.Regen = 10.001f;
					if (base.transform.localScale.x + (PotReGen.GageAni / 10f - base.transform.localScale.x) / 5f < 1f)
					{
						base.transform.localScale = new Vector2(base.transform.localScale.x + (PotReGen.GageAni / 10f - base.transform.localScale.x) / 5f, 1f);
					}
					else
					{
						base.transform.localScale = new Vector2(1f, 1f);
					}
				}
			}
			else
			{
				PotReGen.Regen += Time.deltaTime * (Mathf.Pow(1.25f, (float)FileManager.PotRegen) - 0.25f);
				PotReGen.GageAni += Time.deltaTime * (Mathf.Pow(1.25f, (float)FileManager.PotRegen) - 0.25f);
				if (base.transform.localScale.x + (PotReGen.GageAni / 10f - base.transform.localScale.x) / 5f > 1f)
				{
					base.transform.localScale = new Vector2(0f, 1f);
					PotReGen.GageAni = PotReGen.Regen;
				}
				else
				{
					base.transform.localScale = new Vector2(base.transform.localScale.x + (PotReGen.GageAni / 10f - base.transform.localScale.x) / 5f, 1f);
				}
			}
		}
	}

	private void PotGen()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(ObjectManager.Pot_Pab) as GameObject;
		gameObject.GetComponent<Pot>().Flour = UnityEngine.Random.Range(1, 4);
		gameObject.GetComponent<Pot>().ValueLevel = UnityEngine.Random.Range(0, FileManager.PotLevel);
		gameObject.transform.parent = GameObject.Find("PotManager").transform;
		float x = (float)UnityEngine.Random.Range(-1000, 1001) * 0.01f;
		switch (gameObject.GetComponent<Pot>().Flour)
		{
		case 1:
			gameObject.transform.position = new Vector2(x, -6.4f);
			break;
		case 2:
			gameObject.transform.position = new Vector2(x, -2.3f);
			break;
		case 3:
			gameObject.transform.position = new Vector2(x, 1.8f);
			break;
		}
	}

	public static float Regen;

	public static float GageAni;

	private GameObject Manager;
}
