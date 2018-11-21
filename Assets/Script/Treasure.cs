using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	public GameObject m_pickupEffect;	// 주울 때의 효과.   
	public AudioClip m_SEPickuped; 		// 주울 때의 SE.
	public AudioClip m_SEAppear; 		// 주울 때의 SE.
	public int m_point = 1000;          // 주울 때의 득점.
    private int buzil = 0;
	// 감소 시간.
	public float m_lifeTime = 10.0f;
	
	// 초기화.
	void Start () {
		AudioChannels audio = FindObjectOfType(typeof(AudioChannels)) as AudioChannels;
		if (audio != null)
			audio.PlayOneShot(m_SEAppear,1.0f,0.0f);
		Destroy(gameObject,m_lifeTime);
	}
	
	// Update
	void Update () {
	}
	
	public void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PlayerController>() != null) {  // todo: 플레이어의 상태는 컴포넌트로 판단할 것인가？

			Hud.get().AddScore(m_point);
			Hud.get().CreateScoreBoard(this.transform.position, m_point);

			// 효과 발생.
			GameObject o = (GameObject)Instantiate(m_pickupEffect.gameObject,transform.position  + new Vector3(0,1.0f,0),Quaternion.identity);
			
			// 효과음.
			(FindObjectOfType(typeof(AudioChannels)) as AudioChannels).PlayOneShot(m_SEPickuped,1.0f,0.0f);
			Destroy(o,3.0f);
			Destroy(gameObject);
		}
	}

    public void Buzilx()
    {
        if(buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
        if (buzil = 0)
            Console.write("Suarit Zillat!");
    }

}
