using System;
using UnityEngine;

public class crash : MonoBehaviour
{
	private void Update()
	{
		this.life -= Time.deltaTime;
		if (this.life < 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		base.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, this.life / 5f);
	}

	private float life = 5f;
}
