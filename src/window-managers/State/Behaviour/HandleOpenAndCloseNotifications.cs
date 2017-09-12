using System;

namespace BeatThat.UI.PanelState
{
	public class HandleOpenAndCloseNotifications : AnimatorControllerBehaviour<WindowManager>
	{
		override protected void BindControllerState()
		{
			Bind<ChangePanel>(PanelNotifications.OPEN, this.openAction);
			Bind<ClosePanel>(PanelNotifications.CLOSE, this.closeAction);
		}

		private void OnClose(ClosePanel p)
		{
			this.controller.Close(p);
		}
		private Action<ClosePanel> closeAction { get { return m_closeAction?? (m_closeAction = this.OnClose); } }
		private Action<ClosePanel> m_closeAction; 


		private void OnOpen(ChangePanel p)
		{
			this.controller.Open(p);
		}
		private Action<ChangePanel> openAction { get { return m_openAction?? (m_openAction = this.OnOpen); } }
		private Action<ChangePanel> m_openAction; 


	}
}
