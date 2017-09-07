﻿using UnityEngine;
using System.Collections;

public class WaspController : AbstractEnemy
{
	public float respawnDartTime = 1.5f;
	public Transform dartPrefab;

	Animator animator;
	SpriteRenderer dartSprite;
	float startTimeDart;

	void Awake ()
	{
		animator = GetComponent<Animator> ();
	}

	// Use this for initialization
	new void Start ()
	{
		base.Start ();
		dartSprite = GameObject.Find (this.name + "/Body/Abdomen/Dart").GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	new void Update ()
	{
		base.Update ();
	}

	public override void Attack ()
	{
		base.Attack ();
		Debug.Log ("Wasp Attack");
		rb.velocity = Vector2.zero;
		animator.SetTrigger ("attack");
	}

	// Called by "wasp_attack" animation
	void ThrowDart ()
	{
		Transform dartProjectile = (Transform)Instantiate (dartPrefab, dartSprite.transform.position, dartSprite.transform.rotation);
		dartSprite.color = new Color (1f, 1f, 1f, 0.0f);
		startTimeDart = Time.time;
		StartCoroutine (RespawnDart (respawnDartTime));
		isMoving = true;
	}

	IEnumerator RespawnDart (float time)
	{
		yield return new WaitForSeconds (0.5f); // waiting time before respawning dart
		Color newColor = dartSprite.color;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / time) {			
			newColor.a = Mathf.Lerp (0, 1, t);
			dartSprite.color = newColor;
			yield return null;
		}
		newColor.a = 1;
		dartSprite.color = newColor;
		isAtacking = false; // ready to attack again
	}
}
