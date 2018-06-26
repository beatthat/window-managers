using System;
using BeatThat.Panels;
using BeatThat.StateControllers;
using UnityEngine;

namespace BeatThat.WindowManagers
{
    public class HandleOpenAndCloseNotifications : AnimatorControllerBehaviour<WindowManager>
	{
		public bool m_debug;

		override protected void BindControllerState()
		{
			Bind<ChangePanel>(PanelNotifications.OPEN, this.openAction);
			Bind<ClosePanel>(PanelNotifications.CLOSE, this.closeAction);
			Bind(PanelNotifications.CLOSE_ALL, this.closeAllAction);
		}

		private void OnCloseAll()
		{
			#if UNITY_EDITOR || DEBUG_UNSTRIP
			if(m_debug) {
				Debug.Log("[" + Time.frameCount + "] OnCloseAll [" + this.Path() + "]...");
			}
			#endif
			this.controller.CloseAll();
		}
		private Action closeAllAction { get { return m_closeAllAction?? (m_closeAllAction = this.OnCloseAll); } }
		private Action m_closeAllAction; 

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



