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
        var frame = QuantumRunner.Default.Game.Frames.Predicted;
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
        ViewContext.startAnimator.Play("Ready");
        yield return new WaitForSeconds(1);
        ViewContext.readyText.SetActive(false);
        
        ViewContext.fightText.SetActive(true);
        ViewContext.startAnimator.Play("GameStart");
        yield return new WaitForSeconds(1);
        ViewContext.fightText.SetActive(false);
    }
    
    private void OnDamage(EventDamage eventDamage)
    {
        var frame = QuantumRunner.Default.Game.Frames.Predicted;
        var character = frame.Get<PlayerCharacter>(eventDamage.CharacterEntity);
        int playerIndex = character.PlayerNumber - 1;
        ViewContext.players[playerIndex].slider.fillAmount = character.PlayerHP.AsFloat / eventDamage.MaxHP;
    }

    private void OnGameEnd(EventGameEnd eventGameEnd)
    {
        StartCoroutine(GameEndCoroutine(eventGameEnd.LoseCharacterNumber));
    }

    IEnumerator GameEndCoroutine(int loseCharacterNumber)
    {
        ViewContext.endText.SetActive(true);
        ViewContext.endAnimator.Play("GameEnd");
        yield return new WaitForSeconds(2);
        ViewContext.endText.SetActive(false);
        
        var frame = QuantumRunner.Default.Game.Frames.Predicted;
        int winnerCharacterNumber = 2 - loseCharacterNumber;
        string playerName = frame.GetPlayerData(winnerCharacterNumber).PlayerNickname;
        ViewContext.winnerText.text = $"{playerName}\n<size=160>WIN</size>";
        ViewContext.winnerText.gameObject.SetActive(true);
        ViewContext.endAnimator.Play("GameWinner");
        
        yield return new WaitForSeconds(1);
        ViewContext.returnMenuButton.SetActive(true);
    }
}
