using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csPlayerController : MonoBehaviour {

	public float walkSpeed = 3.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;
	private Vector3 velocity;

	CharacterController controller = null;
	Animator anim = null;

	bool isAttack = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.isGrounded) {
			velocity = new Vector3 (CrossPlatformInputManager.GetAxis ("Horizontal"), 0, CrossPlatformInputManager.GetAxis ("Vertical"));
			velocity *= walkSpeed;

			//if (CrossPlatformInputManager.GetButton ("Attack") && isAttack == false) {
			//	isAttack = true;
			//	anim.SetBool ("Attack", true);
			//	return;
			//}
			if (CrossPlatformInputManager.GetButton ("Jump")) {

				anim.SetBool ("isAttack", true);
				StartCoroutine (AttackAction ());
			} else {
				if (velocity.magnitude > 0) {
					//GetComponent<Animation> ().CrossFade ("walk", 0.1f);
					anim.SetBool ("isMove", true);
					Debug.Log ("move");
					transform.LookAt (transform.position + velocity);

				} else {
					anim.SetBool ("isMove", false);
					Debug.Log ("not");
					//GetComponent<Animation> ().CrossFade ("iddle", 0.1f);
				}

			}
			//Debug.Log (velocity.magnitude);
		}

		velocity.y -= (gravity * Time.deltaTime);
		controller.Move (velocity * Time.deltaTime);
	}

	IEnumerator AttackAction()
	{


		yield return new WaitForSeconds (0.5f);
		anim.SetBool ("isAttack", false);
	}
}
