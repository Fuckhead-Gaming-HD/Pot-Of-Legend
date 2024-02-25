using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    static Dictionary<string, int> _003C_003Ef__switch_0024map2;
	private void Start()
	{
		this.Black = GameObject.Find("Black");
		this.PotTabImage = Resources.Load<Sprite>("Shop/PotTab");
		this.HeroTabImage = Resources.Load<Sprite>("Shop/HeroTab");
	}

	private void Update()
	{
		if (Shop.OnShop)
		{
			this.OpenShop();
			if (Input.GetMouseButtonDown(0) && Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Tab")))
			{
				string name = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Tab")).collider.name;
				if (name != null)
				{
					if (Shop._003C_003Ef__switch_0024map2 == null)
					{
						Shop._003C_003Ef__switch_0024map2 = new Dictionary<string, int>(2)
						{
							{
								"PotButton",
								0
							},
							{
								"HeroButton",
								1
							}
						};
					}
					int num;
					if (Shop._003C_003Ef__switch_0024map2.TryGetValue(name, out num))
					{
						if (num != 0)
						{
							if (num == 1)
							{
								if (this.PotTab.activeSelf)
								{
									AudioSource.PlayClipAtPoint(FileManager.Click, Vector3.zero);
								}
								base.GetComponent<SpriteRenderer>().sprite = this.HeroTabImage;
								this.HeroTab.SetActive(true);
								this.PotTab.SetActive(false);
							}
						}
						else
						{
							if (this.HeroTab.activeSelf)
							{
								AudioSource.PlayClipAtPoint(FileManager.Click, Vector3.zero);
							}
							base.GetComponent<SpriteRenderer>().sprite = this.PotTabImage;
							this.PotTab.SetActive(true);
							this.HeroTab.SetActive(false);
						}
					}
				}
			}
		}
		else
		{
			this.CloseShop();
		}
	}

	private void OpenShop()
	{
		if (base.transform.position.x < -0.01f)
		{
			Shop.FullShop = false;
			base.transform.position = new Vector2(base.transform.position.x - base.transform.position.x / 8f, 0f);
			this.Black.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f + base.transform.position.x / 23.7f * 0.6f);
		}
		else
		{
			Shop.FullShop = true;
			base.transform.position = Vector2.zero;
			this.Black.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
		}
	}

	private void CloseShop()
	{
		Shop.FullShop = false;
		if (base.transform.position.x > -23.7f)
		{
			base.transform.position = new Vector2(base.transform.position.x - (base.transform.position.x + 23.7f) / 8f, 0f);
			this.Black.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f + base.transform.position.x / 23.7f * 0.6f);
		}
		else
		{
			base.transform.position = new Vector2(-23.7f, 0f);
			this.Black.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
		}
	}

	public static bool HeroTabStart;

	public static bool OnShop;

	public static bool FullShop;

	public GameObject PotTab;

	public GameObject HeroTab;

	private GameObject Black;

	private Sprite PotTabImage;

	private Sprite HeroTabImage;
}
