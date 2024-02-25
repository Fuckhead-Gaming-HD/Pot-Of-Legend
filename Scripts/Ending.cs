using System;
using UnityEngine;

public class Ending : MonoBehaviour
{
	private void Update()
	{
		switch (Ending.anim)
		{
		case 0:
			if (this.delay < 3f)
			{
				if (FileManager.PlayerGold[15] > 0)
				{
					HeroTabScr.SelectIndex = 0;
					this.Moneymillon();
					Ending.DontGold = true;
					GameObject.Find("Gage").GetComponent<PotReGen>().enabled = false;
					this.PotDes();
					this.delay += Time.deltaTime;
				}
			}
			else
			{
				Ending.anim = 1;
				this.delay = 0f;
				this.delay2 = 0f;
				this.gov = 15;
				AudioSource.PlayClipAtPoint(FileManager.Buy, Vector3.zero, 0.7f);
			}
			break;
		case 1:
			if (this.gov >= 0)
			{
				this.delay += Time.deltaTime;
				if (this.delay > 0.1f)
				{
					FileManager.PlayerGold[this.gov] = 0;
					this.gov--;
					this.delay = 0f;
				}
				for (int i = this.gov; i >= 0; i--)
				{
					FileManager.PlayerGold[i] = UnityEngine.Random.Range(0, 10);
				}
			}
			else
			{
				this.delay += Time.deltaTime;
			}
			if (this.delay > 1f)
			{
				Ending.anim = 2;
				this.delay = 0f;
				AudioSource.PlayClipAtPoint(FileManager.Shop, Vector3.zero);
			}
			break;
		case 2:
			if (GameObject.Find("Legend(Clone)").transform.position.y < 0.01f)
			{
				GameObject.Find("Legend(Clone)").transform.position = new Vector2(0f, 0f);
				Ending.anim = 3;
				AudioSource.PlayClipAtPoint(FileManager.Tension, Vector3.zero);
			}
			else
			{
				GameObject.Find("Legend(Clone)").transform.position = new Vector2(0f, GameObject.Find("Legend(Clone)").transform.position.y / 1.05f);
			}
			break;
		case 3:
			this.delay += Time.deltaTime;
			if (this.delay > 0.1f)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(ObjectManager.Patic) as GameObject;
				gameObject.transform.parent = GameObject.Find("Legend(Clone)").transform;
				this.delay = 0f;
			}
			this.delay2 += Time.deltaTime;
			if (this.delay2 > 4.8f && this.onesound)
			{
				AudioSource.PlayClipAtPoint(FileManager.Collect, Vector3.zero);
				this.onesound = false;
			}
			if (this.delay2 > 5f)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate(ObjectManager.end) as GameObject;
				this.collectselect();
				Ending.anim = 4;
				this.delay = 0f;
				this.delay2 = 0f;
				this.onesound = true;
				Ranking.Rank_CollectLegendPot++;
				FileManager.CollectionSave();
				FileManager.DataZero();
			}
			GameObject.Find("Legend(Clone)").transform.position = new Vector2((UnityEngine.Random.Range(0f, this.delay2 * 10f) - this.delay2 * 5f) / 100f, (UnityEngine.Random.Range(0f, this.delay2 * 10f) - this.delay2 * 5f) / 100f);
			break;
		case 4:
			this.delay += Time.deltaTime;
			if (this.delay > 0.1f)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(ObjectManager.Patic) as GameObject;
				gameObject3.transform.parent = GameObject.Find("Legend(Clone)").transform;
				this.delay = 0f;
			}
			this.delay2 += Time.deltaTime;
			GameObject.Find("Legend(Clone)").transform.position = Vector2.zero;
			if (this.delay2 > 10f)
			{
				FileManager.MainII();
				CameraMove.GamePlay = 1;
				Ending.anim = 0;
				this.delay = 0f;
				this.delay2 = 0f;
				AudioSource.PlayClipAtPoint(FileManager.Wind, Vector3.zero);
			}
			break;
		}
	}

	private void Moneymillon()
	{
		FileManager.PlayerGold[15] = 1;
		for (int i = 14; i >= 0; i--)
		{
			FileManager.PlayerGold[i] = 0;
		}
	}

	private void PotDes()
	{
		for (int i = GameObject.Find("PotManager").transform.childCount; i > 0; i--)
		{
			GameObject.Find("PotManager").transform.GetChild(i - 1).GetComponent<Pot>().HP = 0;
		}
	}

	private bool fullcollection()
	{
		for (int i = 0; i < 10; i++)
		{
			if (FileManager.Collection[i] == 0)
			{
				return false;
			}
		}
		return true;
	}

	private void collectselect()
	{
		if (!this.fullcollection())
		{
			do
			{
				this.SelectColrect = UnityEngine.Random.Range(0, 10);
			}
			while (FileManager.Collection[this.SelectColrect] != 0);
			FileManager.Collection[this.SelectColrect] = 1;
			GameObject.Find("Legend(Clone)").GetComponent<SpriteRenderer>().sprite = FileManager.Collec[this.SelectColrect];
			GameObject.Find("Legend(Clone)").transform.position = new Vector2(0f, -1.5f);
			GameObject.Find("Legend(Clone)").transform.localScale = new Vector2(10f, 10f);
		}
	}

	public static bool DontGold;

	public static int anim;

	private bool onesound = true;

	private float delay;

	private float delay2;

	private int gov;

	private int SelectColrect;
}
