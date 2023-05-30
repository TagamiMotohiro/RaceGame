using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Reload : Goal_Line //2023”N5Œ28“ú‚ÉC³‚ğs‚¢‚Ü‚µ‚½B
{
	protected override void LineHitCall()
	{
		manager.SetCanGall();
	}
}
