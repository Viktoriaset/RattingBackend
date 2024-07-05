namespace Ratting.Aplication.Battle;

public class BattleRewardConfig
{
    private const int DEFAULT_REWARD = 1;
    private readonly Dictionary<int, int> m_rewardForPosition = new()
    {
        { 1, 20 },
        { 2, 10 }
    };

    public int GetReward(int position)
    {
        if (m_rewardForPosition.ContainsKey(position))
        {
            return m_rewardForPosition[position];
        }

        return DEFAULT_REWARD;
    }
}