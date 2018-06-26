using BeatThat.Properties;
using BeatThat.StateControllers;
using UnityEngine.Events;

namespace BeatThat.WindowManagers
{
    public class MaintainHasActiveWindowParam : AnimatorControllerBehaviour<WindowManager>
	{
		override protected void BindControllerState()
		{
			Bind(this.controller.activeWindowChanged, this.activeWindowChangedAction);
		}

		override protected void DidEnter()
		{
			OnActiveWindowChanged();
		}

		private void OnActiveWindowChanged()
		{
			this.controller.SetBool<HasActiveWindow>(this.controller.hasActivePanel);
		}
		private UnityAction activeWindowChangedAction { get { return m_activeWindowChangedAction?? (m_activeWindowChangedAction = this.OnActiveWindowChanged); } }
		private UnityAction m_activeWindowChangedAction;
	}
}

