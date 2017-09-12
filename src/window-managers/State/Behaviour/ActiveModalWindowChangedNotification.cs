using UnityEngine.Events;

namespace BeatThat.UI.PanelState
{
	public class ActiveModalWindowChangedNotification : AnimatorControllerBehaviour<WindowManager>
	{
		public bool m_sendOnEnterState = false;

		override protected void BindControllerState()
		{
			Bind(this.controller.activeWindowChanged, this.activeWindowChangedAction);
		}

		override protected void DidEnter()
		{
			if(m_sendOnEnterState) {
				OnActiveWindowChanged();
			}
		}

		private void OnActiveWindowChanged()
		{
			PanelNotifications.ActiveModalWindowChanged(new ManagedPanel(this.controller.activePanel, this.controller.gameObject));
		}
		private UnityAction activeWindowChangedAction { get { return m_activeWindowChangedAction?? (m_activeWindowChangedAction = this.OnActiveWindowChanged); } }
		private UnityAction m_activeWindowChangedAction; 
	}
}
