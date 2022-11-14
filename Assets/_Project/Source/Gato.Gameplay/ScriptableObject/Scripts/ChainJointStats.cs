using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Chain Joint Stats")]
public class ChainJointStats : ScriptableObject
{
	[Header ("Stats")]
	public float shotSpeed;
	public float distanceMultiplier;
}
