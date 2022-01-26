using System.Collections.Generic;
using UnityEngine;

namespace FactoryAssault
{
    public class StoragePointManager : MonoBehaviour
    {
		public ResourceType typeOfResources;
        [SerializeField] private List<StoragePoint> storagePoints;
		[SerializeField] private int storagePointCapacity = 7;

		[SerializeField] private bool isExit;

		private float sendingDuration = 0.4f;
		private float sendingTimer = 0f;

		private void Update()
		{
			sendingTimer += Time.deltaTime;
		}

		public bool GetResources(ResourcesType res)
		{
			for (int i = 0; i < storagePoints.Count; i++)
			{
				if (storagePoints[i].GetCountOfResources() < storagePointCapacity)
				{
					storagePoints[i].GetResource(res);
					return true;
				}
			}

			return false;
		}

		public ResourcesType GiveResources()
		{
			for (int i = storagePoints.Count - 1; i >= 0; i--)
			{
				if (storagePoints[i].GetCountOfResources() > 0)
				{
					return storagePoints[i].GiveResource();
				}
			}
			return null;
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (hit.gameObject.CompareTag("Storage") && sendingTimer > sendingDuration)
			{
				StoragePointManager storage = hit.gameObject.GetComponent<StoragePointManager>();
				if (storage.isExit)
				{
					ResourcesType res = storage.GiveResources();
					if (res != null)
					{
						if(!GetResources(res))
						{
							storage.GetResources(res);
						}
					}
				}
				else
				{
					ResourcesType res = GiveResources();
					if (res != null)
					{
						if (storage.typeOfResources == res.type)
						{
							if(!storage.GetResources(res))
							{
								GetResources(res);
							}
						}
						else GetResources(res);
					}
				}

				sendingTimer = 0f;
			}
		}
    }
}
