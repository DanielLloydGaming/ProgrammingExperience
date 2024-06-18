using System.Collections.Generic;
using UnityEngine;

namespace VartraAbyss
{
	public class Storage : MonoBehaviour
	{
		[SerializeField] public bool staticStorage;

		[SerializeField] protected List<UISlot> slots = new();
		[SerializeField] protected List<AbilitySO> items = new();

		UISlot m_swapUISlot;

		private void Start()
		{
			for( int i = 0; i < slots.Count; i++ )
			{
				slots[i].UpdateUI(items[i]);
				slots[i].SetupStorage(this);
				slots[i].SetupMouseDrag(this);
			}
		}

		public void SwapItem(UISlot slot)
		{
			if( m_swapUISlot == null )
			{
				m_swapUISlot = slot;
			}
			else if( m_swapUISlot == slot )
			{
				m_swapUISlot = null;
			}
			else
			{
				Storage storage1 = m_swapUISlot.GetStorage();
				int index1 = storage1.GetItemIndex(m_swapUISlot);
				AbilitySO item1 = storage1.GetItem(index1);

				Storage storage2 = slot.GetStorage();
				int index2 = storage2.GetItemIndex(slot);
				AbilitySO item2 = storage2.GetItem(index2);

				if( !storage1.staticStorage )
				{
					storage1.SetItem(index1 , item2);
					m_swapUISlot.UpdateUI(item2);
				}

				if( !storage2.staticStorage )
				{
					storage2.SetItem(index2 , item1);
					slot.UpdateUI(item1);
				}

				m_swapUISlot = null;
			}
		}

		public void ClearSwap() => m_swapUISlot = null;

		public int GetItemIndex(UISlot slot) => slots.IndexOf(slot);
		public AbilitySO GetItem(int index) => items[index];
		void SetItem(int index , AbilitySO item) => items[index] = item;
	}
}
