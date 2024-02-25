using System;
using UnityEngine;

public class Plarticlef : MonoBehaviour
{
	private void Start()
	{
		base.transform.localScale = new Vector2((float)UnityEngine.Random.Range(10, 41) / 100f, 0.5f);
		base.transform.Rotate(new Vector3(0f, 0f, (float)UnityEngine.Random.Range(0, 360)));
		this.Rot = (float)UnityEngine.Random.Range(0, 100) / 100f - 0.5f;
	}

	private void Update()
	{
		base.transform.Rotate(0f, 0f, this.Rot);
		this.life += Time.deltaTime;
		if (this.life <= 0.4f)
		{
			this.Alpha = this.life / 0.4f;
			this.colorlife();
		}
		else if (this.life > 1f)
		{
			this.Alpha = 2f - this.life;
			this.colorlife();
			if (this.life > 2f)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}
	}

	private void colorlife()
	{
		base.GetComponent<SpriteRenderer>().color = new Color(base.GetComponent<SpriteRenderer>().color.r, base.GetComponent<SpriteRenderer>().color.g, base.GetComponent<SpriteRenderer>().color.b, this.Alpha);
	}

	private float life;

	private float Alpha;

	private float Rot;
}
