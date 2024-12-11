using Quantum;

public class PlayerHPView : QuantumEntityViewComponent<PlayerViewContext>
{
    private void Start() 
    {
        var game = QuantumRunner.Default.Game;
        var frame = game.Frames.Predicted;
        var player = frame.Get<PlayerLink>(EntityRef).PlayerRef;
        if (frame.TryGet(EntityView.EntityRef, out PlayerCharacter character))
        {
            if (character.PlayerNumber == 1)
            {
                // RuntimePlayerからプレイヤー名を取得して表示する
                ViewContext.player1.nameLabel.text = frame.GetPlayerData(player).PlayerNickname;
            }else if (character.PlayerNumber == 2)
            {
                // RuntimePlayerからプレイヤー名を取得して表示する
                ViewContext.player2.nameLabel.text = frame.GetPlayerData(player).PlayerNickname;
            }
        }
        // UnityのUpdate()のタイミングで安全に実行されるコールバック
        //QuantumCallback.Subscribe(this, (CallbackUpdateView _) => UpdateView());
    }
    /*
    private void OnDisable() {
        QuantumCallback.UnsubscribeListener(this);
    }
    */
    public override void OnUpdateView()
    {
        ChangeSilder();
    }

    private void ChangeSilder()
    {
        var frame = QuantumRunner.Default.Game.Frames.Predicted;
        if (frame.TryGet(EntityView.EntityRef, out PlayerCharacter character))
        {
            var config = frame.FindAsset(frame.RuntimeConfig.GameConfig);
            if (character.PlayerNumber == 1)
            {
                ViewContext.player1.slider.fillAmount = character.PlayerHP.AsFloat / config.MaxHP;
            }else if (character.PlayerNumber == 2)
            {
                ViewContext.player2.slider.fillAmount = character.PlayerHP.AsFloat / config.MaxHP;
            }
        }
    }
}
