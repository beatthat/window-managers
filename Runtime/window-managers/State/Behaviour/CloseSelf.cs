using BeatThat.Controllers;
using BeatThat.Panels;
using BeatThat.StateControllers;

namespace BeatThat.WindowManagers
{
    public class CloseSelf : AnimatorControllerBehaviour<IController>
	{
		override protected void DidEnter()
		{
			PanelNotifications.Close(this.controller);
		}

	}
}


