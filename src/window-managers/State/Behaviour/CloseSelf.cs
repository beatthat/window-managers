using System;
using BeatThat;

namespace BeatThat.UI.PanelState
{
	public class CloseSelf : AnimatorControllerBehaviour<IController>
	{
		override protected void DidEnter()
		{
			PanelNotifications.Close(this.controller);
		}

	}
}
