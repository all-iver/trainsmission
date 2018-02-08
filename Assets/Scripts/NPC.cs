using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NPC : MonoBehaviour {

    Sequence sequence;
    public float speed = 10;
    public float jumpTime = 0.5f;
    public float jumpPower = 0.5f;
    public float minDelay = 1;
    public float maxDelay = 5;
    public float minDistance = 3;
    public float maxDistance = 10;
    public Collider2D homeCollider;
    SpriteRenderer spriteRenderer;
    public NPCTracker.ID ID;
    private Vector2 speechOffset = new Vector2(0.36f, 3.34f);
    private Vector2 catSpeechOffset = new Vector2(0.65f, 0.89f);

    SpeechBubble speechBubble;
    bool stopped = false;
    bool accused = false;

    bool CheckBounds(Vector2 dest) {
        return homeCollider.OverlapPoint(dest);
    }

    void MoveNext() {
        sequence = DOTween.Sequence();
        Vector2 dest;
        float attempts = 0;
        while (true) {
            dest = transform.position + (Vector3) (Random.insideUnitCircle * Random.Range(minDistance, maxDistance));
            if (CheckBounds(dest))
                break;
            attempts ++;
            if (attempts > 50)
                return;
        }
        sequence.SetDelay(Random.Range(minDelay, maxDelay));
        float duration = Vector2.Distance(transform.position, dest) / speed;
        // sequence.Append(transform.DOMove(dest, duration));
        sequence.Append(transform.DOJump(dest, jumpPower, Mathf.CeilToInt(duration / jumpTime), duration).SetEase(Ease.Linear)
                .OnStart(() => {
                    spriteRenderer.flipX = dest.x - transform.position.x > 0 ? false : true;
                }));
        sequence.OnComplete(MoveNext);
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (ID == NPCTracker.ID.None)
        {
			//ghost cat for unknown NPCs
			spriteRenderer.sprite = NPCTracker.GetSprite(NPCTracker.ID.Cat);
			spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
        else
        {
            spriteRenderer.sprite = NPCTracker.GetSprite(ID);
        }

        if (!homeCollider) {
            Debug.Log("NPC can't move without a home collider");
            return;
        }
        InitSpeech();
        MoveNext();
    }

    public SpeechHelper.TraincarID GetTraincar()
    {
        var traincar = GetComponentInParent<Traincar>();
        return
            traincar == null
            ? SpeechHelper.TraincarID.Caboose
            : traincar.ID
        ;
    }

    #region speech AI
    Sprite speechIcon1;
    Sprite speechIcon2;
	SpeechEmotion speechEmotion;
    NPC Accused;

    private void InitSpeech()
    {
        speechIcon1 = SpeechHelper.UnknownSprite;
        speechIcon2 = SpeechHelper.UnknownSprite;
		
        bool isCulprit = ID == NPCTracker.Culprit;
        bool sameTraincar = NPCTracker.FindCulprit().GetTraincar() == GetTraincar();

		#region Special Case: Cat
		if (ID == NPCTracker.ID.Cat)
		{
			if (isCulprit) speechEmotion = SpeechEmotion.Loopy;
			else if (sameTraincar) speechEmotion = SpeechEmotion.Scared;
			else speechEmotion = SpeechEmotion.Loopy;

			Accused = NPCTracker.FindCulprit();
			speechIcon1 = SpeechHelper.GetIcon_Person(NPCTracker.ID.Cat);
			speechIcon2 = SpeechHelper.GetIcon_Certainty(false);
			return;
		}
		#endregion

		//base certainty
		float certainty = sameTraincar ? 1.0f : 0.9f;

        //accuse somebody
        float rng_wrongAccusation =
            isCulprit ? 1.0f :
            sameTraincar ? 0.0f :
            Random.Range(0.0f, 1.0f)
        ;

        if (rng_wrongAccusation < 0.5f)
        {
            Accused = NPCTracker.FindCulprit();
        }
        else if (rng_wrongAccusation < 0.75f)
        {
            accused = NPCTracker.FindNPC(NPCTracker.GetRandomNPCID());
            certainty -= 0.5f;
        }
        else
        {
            Accused = NPCTracker.FindNPC(NPCTracker.RedHerring);
            certainty -= 0.3f;
        }

        //should never happen; just for safety
        if (Accused == this || Accused == null)
            Accused = NPCTracker.FindNPC(NPCTracker.RedHerring);

        //first icon represents accusation
        float rng_vagueness =
            sameTraincar ? Random.Range(0.0f, 0.6f) :
            Random.Range(0.0f, 1.0f)
        ;
        if (rng_vagueness < 0.4f)
        {
            speechIcon1 = SpeechHelper.GetIcon_Person(Accused.ID);
            certainty -= 0.2f;
        }
        else if (rng_vagueness < 0.6f)
        {
            speechIcon1 = SpeechHelper.GetIcon_Gender(Accused.ID);
        }
        else
        {
            speechIcon1 = SpeechHelper.GetIcon_Car(Accused.GetTraincar());
        }

        //second icon and animation represent certainty
        float rng_certainty = Random.Range(0.0f, certainty);

        if (rng_certainty < 0.2f)
        {
            speechIcon2 = SpeechHelper.GetIcon_Certainty(false);
			speechEmotion = SpeechEmotion.Loopy;
		}
        else if (rng_certainty < 0.5f)
        {
            speech2IsDirection = true;
			speechEmotion = SpeechEmotion.Loopy;
		}
		else if (rng_certainty < 0.7f)
		{
			speech2IsDirection = true;
			speechEmotion = SpeechEmotion.Scared;
		}
		else
        {
            speechIcon2 = SpeechHelper.GetIcon_Certainty(true);
			speechEmotion = SpeechEmotion.Emphatic;
		}
    }

    private bool speech1IsDirection;
    private bool speech2IsDirection;
    private void UpdateSpeech()
    {

        if (speech1IsDirection)
        {
            speechIcon1 = SpeechHelper.GetIcon_Direction(Accused.transform.position.x - transform.position.x);
        }

        if (speech2IsDirection)
        {
            speechIcon2 = SpeechHelper.GetIcon_Direction(Accused.transform.position.x - transform.position.x);
        }
    }
    #endregion


    public void StopAndSpeak(Vector2 playerPos) {
        if (stopped)
            return;

        float dir = transform.position.x - playerPos.x;
        spriteRenderer.flipX = dir < 0 ? false : true;
        if (sequence != null) {
            sequence.Kill();
            sequence = null;
        }
        stopped = true;
        Vector2 off = speechOffset;
        if (spriteRenderer.sprite.name == "cat")
            off = catSpeechOffset;
		
        speechBubble = FindObjectOfType<SpeechSpawner>().SpawnBubble((Vector2)transform.position + off, transform);
		UpdateSpeech();
		speechBubble.Say(speechIcon1, speechIcon2, speechEmotion);
    }

    public void BecomeAccused(Vector2 playerPos) {
        accused = true;
        if (speechBubble) {
            speechBubble.Close();
            speechBubble.gameObject.SetActive(false);
            speechBubble = null;
        }
        float dir = transform.position.x - playerPos.x;
        Vector2 off = new Vector2(dir < 0 ? -1 : 1, 0);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(transform.position + (Vector3) off, 2f, 1, 0.2f));
        sequence.Append(transform.DOJump(transform.position + (Vector3) off * 1.5f, 1f, 1, 0.2f));
        float r = dir < 0 ? 20 : -20;
        transform.DORotate(new Vector3(0, 0, r), 0.2f);
    }

    public void EndSpeaking() {
        if (speechBubble) {
            speechBubble.Close();
            speechBubble = null;
        }
        stopped = false;
        MoveNext();
    }
}
