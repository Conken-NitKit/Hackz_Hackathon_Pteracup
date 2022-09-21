using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeAnimation : MonoBehaviour
{
	[SerializeField]
	Fade fade = null;

	[SerializeField]
	private Image backGround;

	[SerializeField]
	private InputField inputField;

	[SerializeField]
	private Text parameterText;
	[SerializeField]
	private Text rankText;

	[SerializeField]
	private float fadeTime = 2f;
	[SerializeField]
	private float excutionTime = 1f;
	[SerializeField]
	private float inputFieldPositionX = 270;
	[SerializeField]
	private float inputFieldPositionY = 90;
	[SerializeField]
	private float parameterTextPositionX = 195;
	[SerializeField]
	private float parameterTextPositionY = -110;
	[SerializeField]
	private float rankTextPositionX = 0;
	[SerializeField]
	private float rankTextPositionY = 174;

	private float positionZ = 0;
	private float fadeValue = 1;

	private string akagami = "FFFFFF";



	public void OnClick()
	{
		fade.FadeIn(2, () =>
		{
			fade.FadeOut(1);
		});
		StartCoroutine("FadeInParameter");
	}

	IEnumerator FadeInParameter()
    {
		yield return new WaitForSeconds(fadeTime);

		/*
		Texture2D texture = new Texture2D(200, 200);
		texture.filterMode = FilterMode.Trilinear;
		texture.LoadImage(bytes);

		rawImage.texture = texture;
		*/
		yield return new WaitForSeconds(excutionTime);

		backGround.DOFade(fadeValue, excutionTime);

		yield return new WaitForSeconds(excutionTime);

		inputField.text = $"{akagami}”¯‚ÌƒVƒƒƒ“ƒNƒX";
		rankText.transform.DOLocalMove(new Vector3(rankTextPositionX, rankTextPositionY, positionZ), excutionTime);
		inputField.transform.DOLocalMove(new Vector3(inputFieldPositionX, inputFieldPositionY, positionZ), excutionTime);
		parameterText.transform.DOLocalMove(new Vector3(parameterTextPositionX, parameterTextPositionY, positionZ), excutionTime);

		yield return new WaitForSeconds(excutionTime);
		

	}

}
