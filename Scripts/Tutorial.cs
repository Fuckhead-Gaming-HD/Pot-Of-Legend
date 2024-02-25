using System;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    static Dictionary<string, int> _003C_003Ef__switch_0024map4;
	private void Start()
	{
		for (int i = 0; i < 7; i++)
		{
			Tutorial.TutorialImage[i] = Resources.Load<Sprite>("Tutorial/tutorial" + i);
		}
		GameObject.Find("Gage").GetComponent<PotReGen>().enabled = false;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Tutorial")))
		{
			string name = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition), Vector2.zero, float.PositiveInfinity, 1 << LayerMask.NameToLayer("Tutorial")).collider.name;
			if (name != null)
			{
				if (Tutorial._003C_003Ef__switch_0024map4 == null)
				{
					Tutorial._003C_003Ef__switch_0024map4 = new Dictionary<string, int>(2)
					{
						{
							"next",
							0
						},
						{
							"prev",
							1
						}
					};
				}
				int num;
				if (Tutorial._003C_003Ef__switch_0024map4.TryGetValue(name, out num))
				{
					if (num != 0)
					{
						if (num == 1)
						{
							if (this.Index > 0)
							{
								AudioSource.PlayClipAtPoint(FileManager.Click, Vector3.zero);
								this.Index--;
							}
						}
					}
					else if (this.Index < 6)
					{
						AudioSource.PlayClipAtPoint(FileManager.Click, Vector3.zero);
						this.Index++;
					}
					else
					{
						AudioSource.PlayClipAtPoint(FileManager.Click, Vector3.zero);
						UnityEngine.Object.Destroy(base.gameObject);
						GameObject.Find("Gage").GetComponent<PotReGen>().enabled = true;
					}
				}
			}
		}
		base.GetComponent<SpriteRenderer>().sprite = Tutorial.TutorialImage[this.Index];
	}

	public static Sprite[] TutorialImage = new Sprite[7];

	private int Index;
}
