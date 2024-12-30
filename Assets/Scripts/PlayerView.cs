using System.Collections;
using System.Text;
using Quantum;
using UnityEngine;

public class PlayerView : QuantumSceneViewComponent<PlayerViewContext>
{
    private int frameCount = 0;
    private void Start() 
    {
        QuantumEvent.Subscribe<EventPlayerAdd>(this, OnPlayerAdded);
        QuantumEvent.Subscribe<EventDamage>(this, OnDamage);
        QuantumEvent.Subscribe<EventPunch>(this, OnPunch);
        QuantumEvent.Subscribe<EventGameEnd>(this, OnGameEnd);
    }

    public override void OnUpdateView()
    {
        StringBuilder builder = new StringBuilder("Waiting for Player.");
        for (int i = 0; i < frameCount / 60 % 3; i++)
        {
            builder.Append(".");
        }
        ViewContext.waitingText.text = builder.ToString();
        frameCount++;
    }

    private void OnPlayerAdded(EventPlayerAdd eventGameStart)
    {
        var frame = QuantumRunner.Default.Game.Frames.Verified;

        int playerIndex = eventGameStart.PlayerNumber - 1;
        ViewContext.players[playerIndex].nameLabel.text = frame.GetPlayerData(playerIndex).PlayerNickname;

        if (eventGameStart.PlayerNumber == 2)
        {
            StartCoroutine(GameStartCoroutine());
        }
    }

    IEnumerator GameStartCoroutine()
    {
        // StartCoroutine(AudioFadeOutCoroutine(0.5f));
        yield return new WaitForSeconds(0.5f);
        ViewContext.audioSource.volume = 1;
        ViewContext.waitingText.gameObject.SetActive(false);
        ViewContext.readyText.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[0]);
        ViewContext.startAnimator.Play("Ready");
        yield return new WaitForSeconds(1);
        ViewContext.readyText.SetActive(false);
        
        // 音楽入れたら安っぽくなったのでコメントアウト
        // ViewContext.audioSource.clip = ViewContext.audioClips[5];
        // ViewContext.audioSource.Play();
        ViewContext.fightText.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[1]);
        ViewContext.startAnimator.Play("GameStart");
        yield return new WaitForSeconds(1);
        ViewContext.fightText.SetActive(false);
    }
    
    IEnumerator AudioFadeOutCoroutine(float duration)
    {
        for (float f = 0; f < duration; f += Time.deltaTime)
        {
            ViewContext.audioSource.volume = 1 - f / duration;
            yield return null;
        }
        ViewContext.audioSource.Stop();
    }
    
    private void OnDamage(EventDamage eventDamage)
    {
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[4]);
        int playerIndex = eventDamage.CharacterNumber - 1;
        ViewContext.players[playerIndex].slider.fillAmount = eventDamage.PlayerHP.AsFloat / eventDamage.MaxHP;
    }
    
    private void OnPunch(EventPunch eventPunch)
    {
        StartCoroutine(PunchCoroutine(eventPunch.CharacterNumber, eventPunch.RecoveryTime.AsFloat));
    }
    
    IEnumerator PunchCoroutine(int characterNumber, float recoveryTime)
    {
        for (float t = 0; t < recoveryTime; t += Time.deltaTime)
        {
            ViewContext.players[characterNumber - 1].coolTime.fillAmount = t / recoveryTime;
            yield return null;
        }

        ViewContext.players[characterNumber - 1].coolTime.fillAmount = 1;
    }
    
    private void OnGameEnd(EventGameEnd eventGameEnd)
    {
        StartCoroutine(GameEndCoroutine(eventGameEnd.LoseCharacterNumber));
    }

    IEnumerator GameEndCoroutine(int loseCharacterNumber)
    {
        ViewContext.endText.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[2]);
        ViewContext.endAnimator.Play("GameEnd");
        yield return new WaitForSeconds(2);
        ViewContext.endText.SetActive(false);
        
        var frame = QuantumRunner.Default.Game.Frames.Verified;
        int winnerCharacterNumber = 2 - loseCharacterNumber;
        string playerName = frame.GetPlayerData(winnerCharacterNumber).PlayerNickname;
        ViewContext.winnerText.text = $"{playerName}\n<size=160>WIN</size>";
        ViewContext.winnerText.gameObject.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[3]);
        ViewContext.endAnimator.Play("GameWinner");
        
        yield return new WaitForSeconds(1);
        ViewContext.returnMenuButton.SetActive(true);
    }
}
