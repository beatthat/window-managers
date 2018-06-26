using BeatThat.Controllers;
using BeatThat.Panels;
using BeatThat.StateControllers;

namespace BeatThat.WindowManagers
{
    public class CloseActivePanel : AnimatorControllerBehaviour<IController>
	{
		override protected void DidEnter()
		{
			PanelNotifications.Close();
		}

	}
}


