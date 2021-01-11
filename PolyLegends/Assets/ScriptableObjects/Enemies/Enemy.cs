using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "New Enemy")]
public class Enemy : ScriptableObject
{
    public GameObject gameObject;

    public string enemyName;
    public string description;

    public float health;
    public float moveSpeed;

    public float baseDamage;

    // maybe create base stats? idk what do you need on the enemies?
}
