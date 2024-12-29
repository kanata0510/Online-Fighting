using System.Collections;
using System.Text;
using Quantum;
using UnityEngine;

public class PlayerView : QuantumEntityViewComponent<PlayerViewContext>
{
    private int frameCount = 0;
    private void Start() 
    {
        PlayerAdded(EntityRef);
        
        QuantumEvent.Subscribe<EventGameStart>(this, OnGameStart);
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
    
    private void OnDisable() {
        QuantumEvent.UnsubscribeListener(this);
    }
    
    private void OnGameStart(EventGameStart eventGameStart)
    {
        PlayerAdded(eventGameStart.PlayerEntity);
    }

    private void PlayerAdded(EntityRef addPlayerEntity)
    {
        var frame = QuantumRunner.Default.Game.Frames.Verified;
        var player = frame.Get<PlayerLink>(addPlayerEntity).PlayerRef;
        if (frame.TryGet(addPlayerEntity, out PlayerCharacter character))
        {
            int playerIndex = character.PlayerNumber - 1;
            ViewContext.players[playerIndex].nameLabel.text = frame.GetPlayerData(player).PlayerNickname;
            if (character.PlayerNumber == 2)
            {
                StartCoroutine(GameStartCoroutine());
            }
        }
    }
    
    IEnumerator GameStartCoroutine()
    {
        ViewContext.waitingText.gameObject.SetActive(false);
        ViewContext.readyText.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[0]);
        ViewContext.startAnimator.Play("Ready");
        yield return new WaitForSeconds(1);
        ViewContext.readyText.SetActive(false);
        
        ViewContext.fightText.SetActive(true);
        ViewContext.audioSource.PlayOneShot(ViewContext.audioClips[1]);
        ViewContext.startAnimator.Play("GameStart");
        yield return new WaitForSeconds(1);
        ViewContext.fightText.SetActive(false);
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
