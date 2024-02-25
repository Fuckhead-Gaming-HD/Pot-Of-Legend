using System;
using UnityEngine;

public class Gold : MonoBehaviour
{
	private void Start()
	{
		this.Target = GameObject.Find("MyGold");
		this.Rot = UnityEngine.Random.Range(50, 100) * 0.1f;
	}

	private void Update()
	{
		if (Shop.OnShop)
		{
			if (!this.Pick)
			{
				GetComponent<Rigidbody2D>().gravityScale = 0f;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				this.RePus = true;
			}
		}
		else
		{
			if (!this.Pick)
			{
				GetComponent<Rigidbody2D>().gravityScale = 1f;
			}
			this.DeLimit += Time.deltaTime;
			if (this.DeLimit > 10f)
			{
				GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, GetComponent<SpriteRenderer>().color.a - 0.03f);
			}
			else if (Input.GetMouseButton(0) && !Ending.DontGold && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y))) < 1.5f && !this.Pick)
			{
				this.Get();
			}
			if (GetComponent<SpriteRenderer>().color.a < 0f)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			transform.Rotate(0f, this.Rot, 0f);
			if (this.Pick)
			{
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				Vector2 vector = new Vector2(this.Target.transform.position.x - transform.position.x, this.Target.transform.position.y - transform.position.y);
				transform.position = new Vector2(transform.position.x + vector.x / 12f, transform.position.y + vector.y / 12f);
				if (Vector2.Distance(transform.position, this.Target.transform.position) < 0.5f)
				{
					FileManager.PlayerGold[this.ValueLevel]++;
					CointSOund.PlayTime = 0.1f;
					FileManager.DataSave();
					UnityEngine.Object.Destroy(gameObject);
				}
			}
			if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
			{
				this.ReVec = GetComponent<Rigidbody2D>().velocity;
			}
			if (this.RePus)
			{
				GetComponent<Rigidbody2D>().AddForce(this.ReVec * 50f);
				this.RePus = false;
			}
		}
	}

	public void Get()
	{
		this.Pick = true;
		GetComponent<Rigidbody2D>().gravityScale = 0f;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GetComponent<Collider2D>().isTrigger = true;
		AudioSource.PlayClipAtPoint(FileManager.Get, Vector3.zero);
	}

	public int ValueLevel;

	public GameObject Target;

	private Vector2 ReVec;

	private AudioClip Coin;

	private float DeLimit;

	private float Rot;

	private bool Pick;

	private bool RePus;
}
