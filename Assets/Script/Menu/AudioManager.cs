using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	public static AudioManager audioManager;
	[SerializeField] private AudioClip[] audios = null;
	public AudioSource bgmSource = null, sfxSource = null;
	private static bool isBGM = true, isSFX = true;
	[SerializeField] private Image bgmImg = null, sfxImg = null;
	[SerializeField] private Sprite musicOn = null, musicOff = null, soundOn = null, soundOff = null;

	public enum Sfx{
		button
	}

	private void Awake(){
		audioManager = this;
		isBGM = true;
		isSFX = true;
	}

	private void Start(){
		if (PlayerPrefs.GetFloat ("default") == 0) {
			PlayerPrefs.SetFloat ("default", 1);
			PlayerPrefs.SetFloat ("isBGM", 1);
			PlayerPrefs.SetFloat ("isSFX", 1);
		}
		if (PlayerPrefs.GetFloat ("isBGM") == 1) {
			isBGM = true;
		} else {
			isBGM = false;
		}
		if (PlayerPrefs.GetFloat ("isSFX") == 1) {
			isSFX = true;
		} else {
			isSFX = false;
		}
		if (isBGM) {
			if (bgmImg != null) {
				bgmImg.sprite = musicOn;
			}
			bgmSource.volume = 1;
		} else {
			if (bgmImg != null) {
				bgmImg.sprite = musicOff;
			}
			bgmSource.volume = 0;
		}
		if (isSFX) {
			if (sfxImg != null) {
				sfxImg.sprite = soundOn;
			}
			sfxSource.volume = 1f;
		} else {
			if (sfxImg != null) {
				sfxImg.sprite = soundOff;
			}
			sfxSource.volume = 0;
		}
	}

	public void PlaySfx(Sfx sound){
		if (sound == Sfx.button)
		{
			audioManager.sfxSource.PlayOneShot (audioManager.audios [0]);
		}
	}

	public void ToggleBgm(){
		isBGM = !isBGM;
		if (isBGM) {
			PlayerPrefs.SetFloat ("isBGM", 1);
			bgmImg.sprite = musicOn;
			bgmSource.volume = 1;
		} else {
			PlayerPrefs.SetFloat ("isBGM", 0);
			bgmImg.sprite = musicOff;
			bgmSource.volume = 0;
		}
	}

	public void ToggleSfx(){
		isSFX = !isSFX;
		if (isSFX) {
			PlayerPrefs.SetFloat ("isSFX", 1);
			sfxImg.sprite = soundOn;
			sfxSource.volume = 0.75f;
		} else {
			PlayerPrefs.SetFloat ("isSFX", 0);
			sfxImg.sprite = soundOff;
			sfxSource.volume = 0;
		}
	}

	public void PlayButtonSound(){
		audioManager.PlaySfx (Sfx.button);
	}
}
