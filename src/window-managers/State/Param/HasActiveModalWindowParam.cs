using System;


namespace BeatThat.UI
{
	/// <summary>
	/// 
	/// Enables a state machine to transition states based on whether or not a modal window is open
	/// by maintaining a bool property 'hasActiveModalWindow'.
	/// </summary>
	public class HasActiveModalWindowParam : AnimatorControllerBehaviour
	{
		public string m_hasActiveParamName = "hasActiveModalWindow";

		override protected void BindControllerState()
		{
			// TODO: send an init 'request' in case there's a window open when we init?

			Bind(PanelNotifications.ACTIVE_MODAL_WINDOW_CHANGED, this.activeModalWindowChangedAction);
		}

		private void OnActiveModalWindowChanged(ManagedPanel p)
		{
			this.state.SetBool(m_hasActiveParamName, p.anyActivePanel);
		}

		private Action<ManagedPanel> activeModalWindowChangedAction { get { return m_activeModalWindowChangedAction?? (m_activeModalWindowChangedAction = this.OnActiveModalWindowChanged); } }
		private Action<ManagedPanel> m_activeModalWindowChangedAction;
	}
}