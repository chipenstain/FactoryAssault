using UnityEngine;
using TMPro;

namespace FactoryAssault
{
    public class Factory : MonoBehaviour
    {
        private enum FactoryType
		{
			Type1,
			Type2,
			Type3
		}

		[SerializeField] private TextMeshProUGUI factoryStatusText;
		[SerializeField] private int factoryId;

		[SerializeField] private FactoryType type;

		[SerializeField] private GameObject resource;

		[SerializeField] private StoragePointManager enterStorage;
		[SerializeField] private StoragePointManager enterStorage2;
		[SerializeField] private StoragePointManager exitStorage;

		[SerializeField] private float durationOfProduction;
		private float productionTimer = 100000f;

		private void Update()
		{
			productionTimer += Time.deltaTime;

			if (productionTimer >= durationOfProduction)
			{
				ResourcesType res;
				switch (type)
				{
					case FactoryType.Type1:
						res = Instantiate(resource, transform.position, transform.rotation).GetComponent<ResourcesType>();
						if (!exitStorage.GetResources(res))
						{
							factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище производимых ресурсов переполнено!";
							Destroy(res.gameObject);
						}
						else
						{
							factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": работает...";
						}
						break;
					case FactoryType.Type2:
						ResourcesType resInter = enterStorage.GiveResources();

						if (resInter != null)
						{
							res = Instantiate(resource, transform.position, transform.rotation).GetComponent<ResourcesType>();

							if (!exitStorage.GetResources(res))
							{	
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище производимых ресурсов переполнено!";
								enterStorage.GetResources(resInter);
								Destroy(res.gameObject);
							}
							else
							{
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": работает...";
								Destroy(resInter.gameObject);
							}
						}
						else
						{
							factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище потребляемых ресурсов пусто!";
						}
						break;
					case FactoryType.Type3:
						ResourcesType resInter1 = enterStorage.GiveResources();
						ResourcesType resInter2 = enterStorage2.GiveResources();

						if (resInter1 != null && resInter2 != null)
						{
							res = Instantiate(resource, transform.position, transform.rotation).GetComponent<ResourcesType>();

							if (!exitStorage.GetResources(res))
							{
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище производимых ресурсов переполнено!";
								enterStorage.GetResources(resInter1);
								enterStorage2.GetResources(resInter2);
								Destroy(res.gameObject);
							}
							else
							{
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": работает...";
								Destroy(resInter1.gameObject);
								Destroy(resInter2.gameObject);
							}
						}
						else if (resInter2 == null && resInter2 == null)
						{
							factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище потребляемых ресурсов '1' и '2' пусто!";
						}
						else
						{
							if (resInter1 != null)
							{
								enterStorage.GetResources(resInter1);
							}
							else
							{
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище потребляемых ресурсов '1' пусто!";
							}
							
							if (resInter2 != null)
							{
								enterStorage2.GetResources(resInter2);
							}
							else
							{
								factoryStatusText.text = "Фабрика " + factoryId.ToString() + ": хранилище потребляемых ресурсов '2' пусто!";
							}
						}
						break;
				}

				productionTimer = 0f;
			}
		}
    }
}
