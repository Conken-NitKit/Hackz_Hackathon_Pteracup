using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
	[SerializeField]
	Fade fade = null;

	public void OnClick()
	{
		fade.FadeIn(2, () =>
		{
			fade.FadeOut(1);
		});
	}
}
