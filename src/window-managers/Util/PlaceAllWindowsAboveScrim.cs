using UnityEngine;
using System.Collections.Generic;

namespace BeatThat.UI
{
	/// <summary>
	/// DisplayListController impl for a modal window system built on Unity Canvas.
	/// Expects to find a scrim object as a child named 'scrim'
	/// </summary>
	public class PlaceAllWindowsAboveScrim : MonoBehaviour, PanelDisplayController 
	{
		public Transform m_scrim;

		public void UpdateDisplayList(IEnumerable<Transform> windowStack)
		{
			if(windowStack == null) {
				ShowScrim(false);
				return;
			}

			var controller = this.transform;

			List<Transform> finalStack = null;
			foreach(var t in windowStack) {
				if(t == null || t.parent != controller) {
					continue;
				}
				if(finalStack == null) {
					finalStack = new List<Transform>();
				}
				finalStack.Add(t);
			}

			if(finalStack == null || finalStack.Count == 0) {
				ShowScrim(false);
				return;
			}

			// unparent all the current child windows, 
			// if we find one not part of the display list, 
			// add it to the front so will remain but far in back
			int insertIx = 0;
			foreach(Transform c in controller) {
				if(!finalStack.Contains(c)) {
					// insert all the unaccounted for objects at the front of the display list, but maintain their order
					finalStack.Insert(insertIx, c);
					insertIx++;
				}
			}

			if(finalStack[finalStack.Count - 1].GetComponent<DisplayWithoutScrim>() == null) {
				Transform myScrim = this.scrim;
				if(myScrim != null) {
					finalStack.Insert(0, myScrim);
				}

				ShowScrim(true);
			}

			// read all the windows to the controller in the new order
			for(int i = 0; i < finalStack.Count; i++) {
				finalStack[i].SetSiblingIndex(i);
			}
		}

		private ShowScrim showScrim { get { return m_showScrim?? (m_showScrim = this.AddIfMissing<ShowScrim>()); } }
		private ShowScrim m_showScrim;

		/// <summary>
		/// We need to maintain a separate 'showScrim' param because some presenters are marked 'DisplayWithoutScrim'
		/// </summary>
		/// <param name="show">If set to <c>true</c> show.</param>
		private void ShowScrim(bool show)
		{
			this.showScrim.value = show;
		}

		public Transform scrim
		{
			get {
				if(m_scrim == null && !m_checkedScrim) {
					m_scrim = this.transform.Find("scrim");
					m_checkedScrim = true;
				}
				return m_scrim;
			}
		}

		private bool m_checkedScrim;
	}
}
