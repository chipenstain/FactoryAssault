using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace FactoryAssault
{
    public class StoragePoint : MonoBehaviour
	{
		private const float HEIGHTOFRESOURCES = 0.31f;
		[SerializeField] private List<ResourcesType> resources = new List<ResourcesType>();

		public int GetCountOfResources()
		{
			return resources.Count;
		}

		public void GetResource(ResourcesType res)
		{
			res.gameObject.transform.DOLocalMove(new Vector3(0, HEIGHTOFRESOURCES * resources.Count, 0), 1f);
			res.gameObject.transform.rotation = transform.rotation;
			res.gameObject.transform.parent = transform;

			resources.Add(res);
		}

		public ResourcesType GiveResource()
		{
			ResourcesType res = resources[resources.Count - 1];
			resources.RemoveAt(resources.Count - 1);
			return res;
		}
	}
}
