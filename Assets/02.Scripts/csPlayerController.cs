using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csPlayerController : MonoBehaviour {

	public float walkSpeed = 3.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;
	private Vector3 velocity;

	public GameObject followManager;
	CharacterController controller = null;
	Animator anim = null;

	bool ismove = true;
	public float thisdis = 0.0f;
	public float maxdis = 100.0f;

	public bool isAttack = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (ismove) {
			if (controller.isGrounded) {
				velocity = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));

				//velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
				velocity *= walkSpeed;

				//if (CrossPlatformInputManager.GetButton ("Attack") && isAttack == false) {
				//	isAttack = true;
				//	anim.SetBool ("Attack", true);
				//	return;
				//}
				if (CrossPlatformInputManager.GetButton ("Attack")) {
					anim.SetBool ("isMove", false);
					anim.SetBool ("isAttack", true);
					isAttack = true;
					transform.LookAt (transform.position + velocity);
				} else {
					isAttack = false;
					anim.SetBool ("isAttack", false);
					if (velocity.magnitude > 0) {
						anim.SetBool ("isMove", true);
						transform.LookAt (transform.position + velocity);

					} else {
						anim.SetBool ("isMove", false);
					}

				}
				//Debug.Log (velocity.magnitude);
			}

			velocity.y -= (gravity * Time.deltaTime);
			controller.Move (velocity * Time.deltaTime);
		}
	}

	public IEnumerator StartArrayMove(ArrayList vec)
	{
		ismove = false;

		//followManager.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ().height = 20.0f;
		//followManager.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ().enabled = false;


		for (int a = 0; a < vec.Count - 1; a++) {
			//transform.position = new Vector3((Vector3)vec [a]);
			Vector3 dir = (Vector3)vec [a + 1] - (Vector3)vec [a];

			float distance1 = Vector3.Distance ((Vector3)vec [a + 1], (Vector3)vec [a]);
			thisdis += distance1;
			if (thisdis > maxdis) {
				ismove = true;
				anim.SetBool ("isSkill", false);
				isAttack = false;
				Debug.Log (a);
				break;
			}

			dir.Normalize ();
			if (dir != Vector3.zero) {
				Quaternion moveQtn = Quaternion.LookRotation (dir);
			
				transform.rotation = Quaternion.Lerp (transform.rotation,
					moveQtn,
					30.0f * Time.deltaTime);
			}

			transform.position = (Vector3)vec [a];
			yield return new WaitForSeconds (0.01f);
		}


		//followManager.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ().height = 10.0f;
		//followManager.GetComponent<UnityStandardAssets.Utility.SmoothFollow> ().enabled = true;
		ismove = true;
	}
}
