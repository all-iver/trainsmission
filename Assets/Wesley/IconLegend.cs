using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IconLegend : MonoBehaviour
{
#pragma warning disable 649
	[SerializeField] GameObject Root;
#pragma warning restore 649

	void Update()
	{
		Root.SetActive(Input.GetKey(KeyCode.X));
	}
}
