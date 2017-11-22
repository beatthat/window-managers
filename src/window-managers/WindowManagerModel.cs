//using System.Collections.Generic;
//using BeatThat.UI;
//using UnityEngine.Events;
//
//
//namespace BeatThat.UI.Panel
//{
//	public class WindowManagerModel
//	{
//		public UnityEvent requestAdded { get { return m_requestAdded; } }
//		public UnityEvent m_requestAdded = new UnityEvent();
//
//		public int requestCount { get { return m_requestStack.Count; } }
//
//		public void AddRequest(ChangePanel request)
//		{
//			m_requestStack.Add(request);
//			this.requestAdded.Invoke();
//		}
//
//		public int TakeRequests(List<ChangePanel> requests)
//		{
//			if(m_requestStack.Count == 0) {
//				return 0;
//			}
//			int before = requests.Count;
//			requests.AddRange(m_requestStack);
//			m_requestStack.Clear();
//			return requests.Count - before;
//		}
//
//		private readonly List<ChangePanel> m_requestStack = new List<ChangePanel>();
//	}
//}