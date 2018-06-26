using BeatThat.CollectionsExt;
using BeatThat.Controllers;
using BeatThat.GameObjectUtil;
using BeatThat.GetComponentsExt;
using BeatThat.OriginalParents;
using BeatThat.Panels;
using BeatThat.StateControllers;
using BeatThat.TransformPathExt;
using UnityEngine;
using UnityEngine.Events;

namespace BeatThat.WindowManagers
{
    /// <summary>
    /// Controls modal windows, which may be stacked or swapped.
    /// 
    /// NOTE regarding InputManager:
    /// 
    /// In BeatThat is common to want to disable the InputManager when a modal window is open.
    /// Assuming ModalWindowManager is using an Animator/StateMachineBehviours, 
    /// you can get the 'disable InputManager when window open' behaviour by adding OnEnterDisableInputOnExitRestore 
    /// to a state that is active when any window is open.
    /// 
    /// 
    /// </summary>
    public class WindowManager : Controller 
	{
		public bool m_disableAutoStart;
		public bool m_debug;

		public UnityEvent activeWindowChanged { get { return m_activeWindowChanged; } set { m_activeWindowChanged = value; } }
		private UnityEvent m_activeWindowChanged = new UnityEvent();

		override protected void UnbindController()
		{
			if(this.isDestroyed) {
				return;
			}

			this.state.SetBool("isActive", false);
		}

		public bool hasActivePanel { get { return this.activePanel != null; } }
		public GameObject activePanel { get { return (m_windowManager != null)? m_windowManager.activePanel: null; } }

		override protected void GoController()
		{
			this.state.SetBool("isActive", true);
		}

		public void CloseAll()
		{
			GameObject p;
			while ((p = this.activePanel) != null) {
				#if UNITY_EDITOR || DEBUG_UNSTRIP
				if(m_debug) {
					Debug.Log("[" + Time.frameCount + "][" + this.Path() + "] will close panel '" + p.name + "'");
				}
				#endif
				Close (new ClosePanel { panelGO = p });
			}
		}

		public void Close(ClosePanel p)
		{
			var activeBefore = this.windowManager.activePanel;

			if (p.panelGO != null) {
				this.windowManager.ClosePanel (p.panelGO, true);
			} 
			else if (p.panelType != null) {
				this.windowManager.ClosePanel (p.panelType, true);
			}
			else {
				this.windowManager.ChangePanel(new ChangePanel(null, null), false, true);
			}

			NotifyActivePanelChange(activeBefore);
		}

		public void Open<T>(object model = null) where T : IController
		{
			Open(ControllerPanelNotifications.NewChangePanel<T>(model));
		}

		public void Open(ChangePanel p)
		{
			var activeBefore = this.windowManager.activePanel;
		
			if(p.panel == null && p.panelType == null) {
				Debug.LogWarning("Request to open window doesn't specify the presenter");
			}

			if(m_debug) {
				Debug.Log("[" + Time.frameCount + "] " + GetType() + " Open called with panel " 
					+ ((p.panel as Component != null)? (p.panel as Component).Path(): "NULL") + " type " + p.panelType);
			}

			ReparentWindowObject(p);
			this.windowManager.ChangePanel(p, false, p.push);

			NotifyActivePanelChange(activeBefore);
		}

		private void NotifyActivePanelChange(GameObject activeBefore)
		{
			if(this.windowManager.activePanel == activeBefore) {
				return;
			}

			this.activeWindowChanged.Invoke();
		}

		override protected void Start()
		{
			base.Start();

			if(m_disableAutoStart) {
				return;
			}

			ResetBindGo();
		}

		private void ReparentWindowObject(ChangePanel p)
		{
			if(p.panel == null || !(p.panel is Component)) {
				return;
			}

			// only reparent if the popup isn't already a child of the popupmanager.
			// this way popups living under anchors maintain their positioning
			if(this.transform.IsAncestorOf((p.panel as Component).transform)) {
				return;
			}

			Transform pt = (p.panel as Component).transform;

			var originalParent = (pt.GetComponent<IOriginalParent>());
			if(originalParent != null) {
				originalParent.EnsureCaptured();
			}

			if(p.controlsOwnPosition) {
				pt.SetParent(this.transform, true);
				return;
			}

			pt.SetParent(this.transform, false);
			pt.localPosition = Vector3.zero;
			pt.localScale = Vector3.one;
			pt.localEulerAngles = Vector3.zero;
		}

		private PanelManager windowManager
		{
			get {
				if(m_windowManager == null) {
					m_windowManager = this.AddIfMissing<ControllerPanelManager>();
					m_windowManager.displayController = this.windowDisplayController;
				}
				return m_windowManager;
			}
		}

		private PanelDisplayController windowDisplayController 
		{
			get { 
				return m_windowDisplayController?? 
				(m_windowDisplayController = this.AddIfMissing<PanelDisplayController, PlaceActiveWindowAboveScrim>());
			} 
		}

		private StateController state { get { return m_state?? (m_state = this.AddIfMissing<StateController, AnimatorController>()); } }
		private StateController m_state;

		private PanelDisplayController m_windowDisplayController;
		private ControllerPanelManager m_windowManager;
	}
}





