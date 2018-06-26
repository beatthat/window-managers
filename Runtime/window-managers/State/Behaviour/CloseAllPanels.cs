using BeatThat.Panels;
using BeatThat.StateControllers;

namespace BeatThat.WindowManagers
{
    public class CloseAllPanels : AnimatorControllerBehaviour
	{
		override protected void DidEnter()
		{
			PanelNotifications.CloseAll();
		}

	}
}


