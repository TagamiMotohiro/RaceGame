using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Reload : Goal_Line //2023年5月28日に修正を行いました。
{
	protected override void LineHitCall()
	{
		manager.SetCanGall();
	}
}
