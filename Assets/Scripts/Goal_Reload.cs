using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Reload : Goal_Line //2023�N5��28���ɏC�����s���܂����B
{
	protected override void LineHitCall()
	{
		manager.SetCanGall();
	}
}
