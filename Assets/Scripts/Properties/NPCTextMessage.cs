using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCTextMessage : MonoBehaviour {
	public Text textName, textMessage;
	public float typingSpeed;
	string tempMessage;
	Animator thisAnim;

	void Awake(){
		thisAnim = GetComponent<Animator>();
		ResetText();
	}

	void ResetText(){
		textMessage.text = textName.text = string.Empty;
	}

	public void Show(string message, string name){
		tempMessage = message;
		textName.text = name;
		thisAnim.SetInteger("State",1);
		StartCoroutine(Animate());
	}

	public void Hide(){
		ResetText();
		thisAnim.SetInteger("State",0);
		ChangeButtonMethod(SkipAnimation);
	}

	public void SkipAnimation(){
		StopAllCoroutines();
		textMessage.text = tempMessage;
		ChangeButtonMethod(Hide);
	}

	IEnumerator Animate(){
		WaitForSeconds delay = new WaitForSeconds(typingSpeed);

		int length = tempMessage.Length;
		textMessage.text = string.Empty;

		for(int i = 0;i<length;i++){
			char temp = tempMessage[i];
			textMessage.text += temp.ToString();
			yield return delay;
		}
		ChangeButtonMethod(Hide);
	}

	void ChangeButtonMethod(UnityEngine.Events.UnityAction action){
		GetComponent<RectTransform>().GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
		GetComponent<RectTransform>().GetChild(0).GetComponent<Button>().onClick.AddListener(action);
	}


}
