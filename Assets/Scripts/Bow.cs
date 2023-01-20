using UnityEngine;
using System.Collections;


namespace DigitalRuby.BowAndArrow
{
	[RequireComponent(typeof(AudioSource))]
	public class Bow : MonoBehaviour
	{
		[Header("Bow Structure")]
		[Tooltip("Bow shaft game object. This is the object that should be rotated and moved to change the bow position in the world.")]
		public GameObject BowShaft;

		[Tooltip("Bow string game object. Do not modify the transform of this object.")]
		public GameObject BowString;

		[Tooltip("Top anchor of the bow, usually attached to the top end of the bow.")]
		public GameObject TopAnchor;

		[Tooltip("Bottom anchor of the bow, usually attached to the bottom end of the bow.")]
		public GameObject BottomAnchor;

		[Tooltip("Used to determine the minimum draw strength of the bow string.")]
		public GameObject MinDrawAnchor;

		[Tooltip("Used to determine the maximum draw strength of the bow string. Distance from this to MinDrawAnchor is compared against how far they actually drew back the bow string to calculate final arrow velocity.")]
		public GameObject MaxDrawAnchor;

		[Header("Bow Firing")]
		[Tooltip("Arrow object to clone when firing.")]
		public GameObject Arrow;

		[Tooltip("How long (in seconds) before the bow can be fired again.")]
		public float Cooldown = 0.75f;

		[Tooltip("Base speed at which arrows leave the bow. Will be lower if the bow is not pulled back all the way.")]
		public float FireSpeed = 80.0f;

		[Tooltip("The number of frames needed to draw the arrow")]
		public int nbDrawFrames = 25;

		[Header("Bow Sounds")]
		[Tooltip("Sounds for knocking arrows")]
		public AudioClip[] KnockClips;

		[Tooltip("Sounds for drawing the bow")]
		public AudioClip[] DrawClips;

		[Tooltip("Sounds for firing the arrow")]
		public AudioClip[] FireClips;


		private LineRenderer bowStringLineRenderer1;
		private LineRenderer bowStringLineRenderer2;
		private AudioSource audioSource;
		private bool drawingBow;
		private GameObject currentArrow;
		private float cooldownTimer;
		private float startAngle;
		private bool drawComplete;
		private int drawFrame;

		private float getAngle(Vector2 pos1, Vector2 pos2)
		{
			return Mathf.Atan2(pos2.y - pos1.y, pos2.x - pos1.x) * Mathf.Rad2Deg;
		}

		private void PlayRandomSound(AudioClip[] clips)
		{
			if (clips != null && clips.Length != 0)
			{
				audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
			}
		}

		private void BeginBowDraw()
		{
			// start drawing back the bow

			// play a knock arrow sound
			PlayRandomSound(KnockClips);

			// create a new arrow that will be fired later
			currentArrow = GameObject.Instantiate(Arrow);
			currentArrow.transform.rotation = BowShaft.transform.rotation;
			currentArrow.SetActive(true);

			// find where the arrow should be based on where they started drawing back the bow
			currentArrow.transform.position = MinDrawAnchor.transform.position;
			drawingBow = true;
			drawComplete = false;
			drawFrame = 0;
		}

		private void ContinueBowDraw()
		{
			Vector3 pos = Vector3.Lerp(MinDrawAnchor.transform.position, MaxDrawAnchor.transform.position, drawFrame / nbDrawFrames);
			currentArrow.GetComponent<Rigidbody2D>().MovePosition(pos);
			currentArrow.GetComponent<Rigidbody2D>().SetRotation(BowShaft.transform.rotation);
			RenderBowString(pos);
			if (!drawComplete)
			{
				// animate arrow
				drawFrame++;
				if (drawFrame >= nbDrawFrames) drawComplete = true;
			}
		}

		private void FireArrow()
		{
			PlayRandomSound(FireClips);
			cooldownTimer = Cooldown;

			// set the current arrow to be a normal physics object (not kinematic) and set it's velocity
			Rigidbody2D arrowRB = currentArrow.GetComponent<Rigidbody2D>();
			Rigidbody2D arrowTipRB = currentArrow.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>();
			arrowRB.isKinematic = arrowTipRB.isKinematic = false;
			arrowRB.velocity = arrowTipRB.velocity = BowShaft.transform.rotation * new Vector2(-FireSpeed, 0.0f);
			Destroy(currentArrow, 3);

			// reset bow state to idle
			drawingBow = false;
			RenderBowString(Vector3.zero);
		}

		private void RenderBowString(Vector3 arrowPos)
		{
			Vector3 startPoint = TopAnchor.transform.position;
			Vector3 endPoint = BottomAnchor.transform.position;

			if (drawingBow)
			{
				bowStringLineRenderer2.gameObject.SetActive(true);
				bowStringLineRenderer1.SetPosition(0, startPoint);
				bowStringLineRenderer1.SetPosition(1, arrowPos);
				bowStringLineRenderer2.SetPosition(0, arrowPos);
				bowStringLineRenderer2.SetPosition(1, endPoint);
			}
			else
			{
				bowStringLineRenderer2.gameObject.SetActive(false);
				bowStringLineRenderer1.SetPosition(0, startPoint);
				bowStringLineRenderer1.SetPosition(1, endPoint);
			}
		}

		private void Start()
		{
			// assign component references for fast use later
			bowStringLineRenderer1 = BowString.transform.GetChild(0).GetComponent<LineRenderer>();
			bowStringLineRenderer2 = BowString.transform.GetChild(1).GetComponent<LineRenderer>();
			audioSource = GetComponent<AudioSource>();
			RenderBowString(Vector3.zero);
			startAngle = BowShaft.transform.eulerAngles.z;
			drawComplete = false;
		}

		private void Update()
		{
			cooldownTimer -= Time.deltaTime;
			// rotate bow
			Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			BowShaft.transform.rotation = Quaternion.Euler(0, 0, getAngle(worldPos, BowShaft.transform.position));

			// draw bow
			if (Input.GetMouseButtonDown(0))
			{
				if (cooldownTimer <= 0.0f) BeginBowDraw();
			}
			else if (Input.GetMouseButton(0))
			{
				ContinueBowDraw();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				if (drawingBow) FireArrow();
			}
			else
			{
				// idle
				RenderBowString(Vector3.zero);
			}
		}


	}
}