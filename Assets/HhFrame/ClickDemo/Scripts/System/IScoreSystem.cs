using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using HhFrame.ClickDemo;
using UnityEngine;

public interface IScoreSystem : ISystem
{
}

public class ScoreSystem : AbstractSystem, IScoreSystem
{
    protected override void OnInit()
    {
        IGameModel gameModel = this.GetModel<IGameModel>();
        this.RegisterEvent<GamePassEvent>(e =>
        {
            Debug.Log("Score = "+gameModel.Score.Value);
            Debug.Log("BestScore = "+gameModel.BestScore.Value);
            if (gameModel.Score.Value > gameModel.BestScore.Value)
            {
                Debug.Log("新记录");
                gameModel.BestScore.Value = gameModel.Score.Value;
            }
        });
        this.RegisterEvent<KillEnemyEvent>(e =>
        {
            gameModel.Score.Value += 10;
            Debug.Log("得分+10");
            Debug.Log("当前分数 = "+gameModel.Score.Value);
        });
        this.RegisterEvent<MissEvent>(e =>
        {
            gameModel.Score.Value += 5;
            Debug.Log("得分-5");
            Debug.Log("当前分数 = "+gameModel.Score.Value);
        });
    }
}
