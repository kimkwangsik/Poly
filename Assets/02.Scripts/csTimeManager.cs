using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class csTimeManager : MonoBehaviour {

	public bool timestop = false;

	public float timestoplimit = 0;


	public ArrayList Vec3ArrayList = new ArrayList ();

	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timestop) {
			timestoplimit += Time.unscaledDeltaTime;


			if (Input.GetButton ("Fire1")) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit[] hits = Physics.RaycastAll (ray);
				for (int i = 0; i < hits.Length; i++) {
					RaycastHit hit = hits [i];
					if (hit.transform.tag.Equals ("Map")) {
						Vec3ArrayList.Add (hit.point);
					}
				}
			}


			if (timestoplimit > 5) {
				Time.timeScale = 1.0f;
				timestoplimit = 0;
				timestop = false;
				StartCoroutine(player.GetComponent<csPlayerController>().StartArrayMove(Vec3ArrayList));
				Vec3ArrayList = new ArrayList ();
			}
		} else {
			if (CrossPlatformInputManager.GetButtonDown ("Skill")) {
				timestop = true;
				Time.timeScale = 0.0f;

			}
			
		}
	}
}
