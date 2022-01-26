using UnityEngine;

namespace FactoryAssault
{
    public class PlayerController : MonoBehaviour
    {
		[SerializeField] private FloatingJoystick joystick;
		private CharacterController controller;
		private Transform aim;

		[SerializeField] private float speed = 15f;

		private void Awake()
		{
			controller = GetComponent<CharacterController>();
			aim = transform.GetChild(0);
		}

		private void Update()
		{
			Vector3 dir = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
			aim.forward = dir;
			controller.SimpleMove(dir * speed);
		}
    }
}
