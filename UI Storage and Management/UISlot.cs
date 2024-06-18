using UnityEngine;
using UnityEngine.UI;
using VartraAbyss.Utility;

namespace VartraAbyss
{
	public class UISlot : MonoBehaviour
	{
		public Image itemImage;
		public Storage storage;
		public MouseDrag mouseDrag;
		public bool isStatic;

		public void SetupStorage(Storage storage)
		{
			this.storage = storage;
		}

		public Storage GetStorage()
		{
			return storage;
		}

		public void UpdateUI(AbilitySO item)
		{
			if( item == null )
			{
				itemImage = null;
				return;
			}

			itemImage.sprite = item.sprite;
		}

		public void SetupMouseDrag(Storage storage)
		{
			mouseDrag = gameObject.GetOrAdd<MouseDrag>();
			mouseDrag.SetupStorage(storage , this);
		}
	}
}
