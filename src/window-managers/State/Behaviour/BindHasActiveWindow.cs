using UnityEngine.Events;


namespace BeatThat.UI
{
	public class BindHasActiveWindow : Subcontroller<WindowManager>
	{
		override protected void BindSubcontroller()
		{
			OnActiveWindowChanged();
			Bind(this.controller.activeWindowChanged, this.activeWindowChangedAction);
		}

		private void OnActiveWindowChanged()
		{
			this.controller.SetBool<HasActiveWindow>(this.controller.hasActivePanel);
		}
		private UnityAction activeWindowChangedAction { get { return m_activeWindowChangedAction?? (m_activeWindowChangedAction = this.OnActiveWindowChanged); } }
		private UnityAction m_activeWindowChangedAction;
	}
}