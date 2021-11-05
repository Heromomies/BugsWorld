using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public static LTDescr delay;
	public string header;
	
	[Multiline()]
	public string content;
	
	public void OnPointerEnter(PointerEventData eventData)
	{
		delay = LeanTween.delayedCall(0.5f, () =>
		{
			TooltipSystem.Show(header, content);
		});
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		LeanTween.cancel(delay.uniqueId);
		TooltipSystem.Hide();
	}
}
