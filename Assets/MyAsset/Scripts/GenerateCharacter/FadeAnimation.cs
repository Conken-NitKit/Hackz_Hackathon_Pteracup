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
	
	private Vector3 monsterEndPosition = new Vector3(-200, -20, 0);
	
	[SerializeField]
	private Image monster;

	private float positionZ = 0;
	private float fadeValue = 1;

	[SerializeField]
	private ColorCode colorCode;
	
	[SerializeField]
	private ParameterDisplay parameterDisplay;

	[SerializeField]
	private MonsterBack monsterBack;

	
	public void Fade()
	{
		fade.FadeIn(2, () =>
		{
			fade.FadeOut(1);
		});
		StartCoroutine(FadeInParameter());
	}

	IEnumerator FadeInParameter()
    {
	    yield return new WaitForSeconds(fadeTime);

	    yield return new WaitForSeconds(excutionTime);

	    parameterDisplay.DecideStatus();

		backGround.DOFade(fadeValue, excutionTime);
		
		yield return new WaitForSeconds(excutionTime);

		inputField.text = $"{colorCode.HexadecimalCenterColor}髪のシャンクス";
		monsterBack.SetMonsterBackColor();
		rankText.transform.DOLocalMove(new Vector3(rankTextPositionX, rankTextPositionY, positionZ), excutionTime);
		inputField.transform.DOLocalMove(new Vector3(inputFieldPositionX, inputFieldPositionY, positionZ), excutionTime);
		parameterText.transform.DOLocalMove(new Vector3(parameterTextPositionX, parameterTextPositionY, positionZ), excutionTime);
		monster.transform.DOLocalMove(monsterEndPosition, excutionTime).SetEase(Ease.OutBack);

		yield return new WaitForSeconds(excutionTime);
    }

}
