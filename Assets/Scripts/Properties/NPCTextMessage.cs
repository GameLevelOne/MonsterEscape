using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCTextMessage : MonoBehaviour {
	public Text textName, textMessage;
	public float typingSpeed;
	string tempMessage;

	public void Show(string message, string name){
		gameObject.SetActive(true);
		tempMessage = message;
		textName.text = name;
		StartCoroutine(Animate());
	}

	public void Hide(){
		gameObject.SetActive(false);
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
	}

	void ChangeButtonMethod(UnityEngine.Events.UnityAction action){
		GetComponent<RectTransform>().GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
		GetComponent<RectTransform>().GetChild(0).GetComponent<Button>().onClick.AddListener(action);
	}


}
