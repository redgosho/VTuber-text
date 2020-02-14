using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// https://memonoana.hatenablog.com/entry/2019/05/22/005823
// JSON構造の明示
[Serializable]
public class InputJson
{
    public VOICE[] voice;
}

[Serializable]
public class VOICE
{
	public string thumbnail;
	public string date;
	public string title;
	public string views;
	public string wav;

}

public class ScrollController : MonoBehaviour {

	[SerializeField]
	RectTransform prefab = null;

	IEnumerator Start () 
	{
		// 入力ファイルはAssets/Resources/test.json
        // input.jsonをテキストファイルとして読み取り、string型で受け取る
        string inputString = Resources.Load<TextAsset>("test").ToString();
        // 上で作成したクラスへデシリアライズ
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);
		// デシリアライズしてforでオブジェクトすべてデバック表示
		for (int i = 0; i < inputJson.voice.Length; i++) {
			// JSON読み込んでいるか確認
			// Debug.Log (inputJson.voice[i].date);
			// Debug.Log (inputJson.voice[i].title);
			// Debug.Log (inputJson.voice[i].views);
			// Debug.Log (inputJson.voice[i].thumbnail);

			// 生成するprefabの読み込み
			var item = GameObject.Instantiate(prefab) as RectTransform;
			item.SetParent(transform, false);

			// Audioの設定
			// wwwクラスのコンストラクタに音声URLを指定
			string audiolUrl = inputJson.voice[i].wav.ToString();

			WWW wwwAudio = new WWW(audiolUrl);
			// 音声ダウンロード完了を待機
			yield return wwwAudio;
			// webサーバから取得した画像をRaw Imagで表示する
			AudioSource audioSource = item.GetComponent<AudioSource>();
			audioSource.clip = wwwAudio.GetAudioClip();

			// テキストの補完
			// テキスト部分を特定
			var texts = item.GetComponentsInChildren<Text>();
			// テキスト部分にJSONのテキストを代入
			texts[0].text = inputJson.voice[i].date.ToString();
			texts[1].text = inputJson.voice[i].title.ToString();
			texts[2].text = inputJson.voice[i].views.ToString();

			// 画像の補完
			// wwwクラスのコンストラクタに画像URLを指定
			string thumbnailUrl = inputJson.voice[i].thumbnail.ToString();

			WWW wwwThumbnai = new WWW(thumbnailUrl);
			// 画像ダウンロード完了を待機
			yield return wwwThumbnai;
			// webサーバから取得した画像をRaw Imagで表示する
			RawImage rawImage = item.GetComponentInChildren<RawImage>();
			rawImage.texture = wwwThumbnai.textureNonReadable;
			//ピクセルサイズ等倍に
			// rawImage.SetNativeSize();
		}
	}
}